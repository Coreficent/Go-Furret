namespace Coreficent.Utility
{
    using UnityEngine;

    public class ApplicationMode : MonoBehaviour
    {
        internal static ApplicationMode _applicationMode = null;

        public ApplicationState Mode = ApplicationState.Debug;
        public enum ApplicationState
        {
            Debug,
            Release
        }

        private void Start()
        {
            _applicationMode = this;
        }

        internal bool DebugMode
        {
            get { return Mode == ApplicationState.Debug; }
        }
    }
}
