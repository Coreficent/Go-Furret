namespace Coreficent.Utility
{
    using UnityEngine;

    public class DebugRender
    {
        public static void Draw(Vector3 start, Vector3 end, Color color)
        {
            if (ApplicationMode._applicationMode.DebugMode)
            {
                Debug.DrawLine(start, end, color);
            }
        }
    }
}

