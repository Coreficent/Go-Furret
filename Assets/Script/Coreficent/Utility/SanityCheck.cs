namespace Coreficent.Utility
{
    using Coreficent.Setting;
    using UnityEngine.SceneManagement;

    public class SanityCheck
    {
        private static readonly string _delimiter = "::";
        public static void Check(object owner, params object[] variables)
        {
            if (ApplicationMode.DebugMode == ApplicationMode.ApplicationState.Debug)
            {
                bool sanityCheckPassed = true;

                foreach (object i in variables)
                {
                    if (i == null || i.ToString() == "null" || i is false)
                    {
                        sanityCheckPassed = false;
                        DebugLogger.Warn(owner + _delimiter + "has an uninitialized variable in" + _delimiter + SceneManager.GetActiveScene().name);
                    }
                }

                if (sanityCheckPassed)
                {
                    DebugLogger.Log(owner + _delimiter + "sanity check passed.");
                }
                else
                {
                    DebugLogger.Warn(owner + _delimiter + "sanity check failed.");
                }
            }
        }
    }
}
