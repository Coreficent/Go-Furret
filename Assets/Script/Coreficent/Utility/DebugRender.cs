namespace Coreficent.Utility
{
    using Coreficent.Setting;
    using UnityEngine;

    public class DebugRender
    {
        public static void Draw(Vector3 start, Vector3 end, Color color)
        {
            if (ApplicationMode.DebugMode == ApplicationMode.ApplicationState.Debug)
            {
                Debug.DrawLine(start, end, color);
            }
        }
    }
}

