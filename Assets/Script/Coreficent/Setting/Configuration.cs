namespace Coreficent.Setting
{
    using UnityEngine;

    public class Configuration : MonoBehaviour
    {
        public static Configuration Singleton;

        public enum ApplicationVersion
        {
            Nova,
            Umbra
        }

        public ApplicationVersion Version = ApplicationVersion.Nova;

        private void Awake()
        {
            Singleton = this;
        }
    }
}
