namespace Coreficent.Utility
{
    using UnityEngine;

    public class ApplicationMode : MonoBehaviour
    {
        internal static ApplicationState DebugMode = ApplicationState.Debug;

        [SerializeField] private ApplicationState _mode = ApplicationState.Debug;

        public enum ApplicationState
        {
            Debug,
            Release
        }

        private void Awake()
        {
            DebugMode = _mode;
            DebugLogger.Awake(this);
        }
    }
}
