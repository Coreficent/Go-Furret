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

            for (var i = 0; i < normals.Length; ++i)
            {
                VertexAttribute cospatial = vertexAttributes[cospatialIndexBuffer[i]];
                normals[i] = cospatial.Normal.normalized;
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

            VertexAttribute inspector = new VertexAttribute
            {
                MergeDistance = _mergeDistance
            };

            for (var i = 0; i < vertices.Length; ++i)
            {
                inspector.Position = vertices[i];

                int vertexIndex = vertexAttributes.IndexOf(inspector);

                if (vertexIndex == -1)
                {
                    indices[i] = vertexAttributes.Count;
                    vertexAttributes.Add(new VertexAttribute()
                    {
                        Position = vertices[i],
                        Normal = Vector3.zero,
                    });
                }
                else
                {
                    indices[i] = vertexIndex;
                }
            }

            return vertexAttributes;
        }

        private void AddWeightedNormal(Vector3 weightedNormal, int vertexIndex, int[] cospatialIndexBuffer, List<VertexAttribute> vertexAttributes)
        {
            vertexAttributes[cospatialIndexBuffer[vertexIndex]].Normal += weightedNormal;
        }
    }
}
