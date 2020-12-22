namespace Coreficent.Utility
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SanityCheck
    {
        private static readonly string _delimiter = "::";
        public static void Check(object owner, params object[] variables)
        {
            if (ApplicationMode._applicationMode.DebugMode)
            {
                bool sanityCheckPassed = true;

                foreach (object i in variables)
                {
                    if (i == null)
                    {
                        sanityCheckPassed = false;
                        Debug.Log(owner + _delimiter + "has an unexpected null variable in" + _delimiter + SceneManager.GetActiveScene().name);
                    }
                }

                if (sanityCheckPassed)
                {
                    Debug.Log(owner + _delimiter + "sanity check passed.");
                }
            }
        }
    }
}
