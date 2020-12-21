﻿namespace Coreficent.Utility
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SanityCheck
    {
        public static bool DebugMode = true;

        public static void Check(object owner, params object[] variables)
        {
            bool sanityCheckPassed = true;
            foreach (object i in variables)
            {
                if (i == null)
                {
                    sanityCheckPassed = false;
                    Debug.Log(owner + "::" + "has an unexpected null variable in" + "::" + SceneManager.GetActiveScene().name);
                }
            }

            if (DebugMode && sanityCheckPassed)
            {
                Debug.Log(owner + "::" + "sanity check passed.");
            }
        }
    }
}