namespace Coreficent.Shading
{
    using System;
    using UnityEngine;
    internal class VertexAttribute : IEquatable<VertexAttribute>
    {
        public Vector3 Position = new Vector3();
        public Vector3 Normal = new Vector3();
        public float MergeDistance = 0.0f;

        public bool Equals(VertexAttribute other)
        {
            if (other == null)
            {
                return false;
            }
            return Vector3.Distance(Position, other.Position) <= MergeDistance;
        }
    }
}
