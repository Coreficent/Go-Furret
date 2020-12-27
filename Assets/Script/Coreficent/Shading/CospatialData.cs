using System.Collections.Generic;

namespace Coreficent.Shading
{
    internal class CospatialData 
    {
        public int[] CospatialIndexBuffer = new int[0];
        public List<CospatialAccumulator> Accumulators = new List<CospatialAccumulator>();
    }
}
