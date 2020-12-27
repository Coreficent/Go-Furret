namespace Coreficent.Shading
{
    using System;
    using UnityEngine;
    internal class CospatialAccumulator : IEquatable<CospatialAccumulator>
    {
        public Vector3 Position = new Vector3();
        public Vector3 Normal = new Vector3();
        public float MergeDistance = 0.0f;

        public bool Equals(CospatialAccumulator other)
        {
            if (other == null)
            {
                return false;
            }
            return Vector3.Distance(Position, other.Position) <= MergeDistance;
        }
    }
}
