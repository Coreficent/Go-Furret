namespace Coreficent.Shading
{
    using System.Collections.Generic;
    using UnityEngine;

    public class NormalRectifier : MonoBehaviour
    {
        [SerializeField] private int _texCoord = 3;
        [SerializeField] private float _mergeDistance = 0.01f;

        private bool UsePrecalculatedNormal
        {
            set
            {
                foreach (Material material in GetComponent<MeshRenderer>().sharedMaterials)
                {
                    material.SetFloat("_PrecalculatedNormal", value ? 1.0f : 0.0f);
                }
            }
        }

        private void Start()
        {
            Mesh mesh = GetComponent<MeshFilter>().mesh;
            Vector3[] vertexBuffer = mesh.vertices;
            int[] indexBuffer = mesh.triangles;

            int[] cospatialIndexBuffer = new int[vertexBuffer.Length];
            List<VertexAttribute> vertexAttributes = CalculateCospatialIndexBuffer(vertexBuffer, cospatialIndexBuffer);

            for (var i = 0; i < indexBuffer.Length / 3; ++i)
            {
                int vertexOffset = i * 3;

                int indexX = indexBuffer[vertexOffset + 0];
                int indexY = indexBuffer[vertexOffset + 1];
                int indexZ = indexBuffer[vertexOffset + 2];

                Vector3 vertexA = vertexBuffer[indexX];
                Vector3 vertexB = vertexBuffer[indexY];
                Vector3 vertexC = vertexBuffer[indexZ];

                Vector3 normal = Vector3.Cross(vertexB - vertexA, vertexC - vertexA).normalized;
                Vector3 weight = new Vector3(Vector3.Angle(vertexB - vertexA, vertexC - vertexA), Vector3.Angle(vertexC - vertexB, vertexA - vertexB), Vector3.Angle(vertexA - vertexC, vertexB - vertexC));

                AddWeightedNormal(normal * weight.x, indexX, cospatialIndexBuffer, vertexAttributes);
                AddWeightedNormal(normal * weight.y, indexY, cospatialIndexBuffer, vertexAttributes);
                AddWeightedNormal(normal * weight.z, indexZ, cospatialIndexBuffer, vertexAttributes);
            }

            Vector3[] normals = new Vector3[vertexBuffer.Length];

            for (int i = 0; i < normals.Length; ++i)
            {
                int cvIndex = cospatialIndexBuffer[i];
                var cospatial = vertexAttributes[cvIndex];
                normals[i] = cospatial.normal.normalized;
            }

            mesh.SetUVs(_texCoord, normals);
            UsePrecalculatedNormal = true;
        }

        private void OnApplicationQuit()
        {
            UsePrecalculatedNormal = false;
        }

        private List<VertexAttribute> CalculateCospatialIndexBuffer(Vector3[] vertices, int[] indices)
        {
            List<VertexAttribute> vertexAttributes = new List<VertexAttribute>();

            for (var i = 0; i < vertices.Length; ++i)
            {
                int vertexIndex = VertexIndexOf(vertices[i], vertexAttributes);

                if (vertexIndex != -1)
                {
                    indices[i] = vertexIndex;
                }
                else
                {
                    indices[i] = vertexAttributes.Count;
                    vertexAttributes.Add(new VertexAttribute()
                    {
                        position = vertices[i],
                        normal = Vector3.zero,
                    });
                }
            }

            return vertexAttributes;
        }

        private int VertexIndexOf(Vector3 position, List<VertexAttribute> registry)
        {
            for (var i = 0; i < registry.Count; i++)
            {
                if (Vector3.Distance(registry[i].position, position) <= _mergeDistance)
                {
                    return i;
                }
            }
            return -1;
        }

        private void AddWeightedNormal(Vector3 weightedNormal, int vertexIndex, int[] cospatialIndexBuffer, List<VertexAttribute> vertexAttributes)
        {
            vertexAttributes[cospatialIndexBuffer[vertexIndex]].normal += weightedNormal;
        }
    }
}
