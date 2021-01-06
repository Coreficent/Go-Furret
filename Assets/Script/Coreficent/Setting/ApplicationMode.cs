namespace Coreficent.Setting
{
    using Coreficent.Utility;
    using UnityEngine;

    public class ApplicationMode : MonoBehaviour
    {
        internal static ApplicationState DebugMode = ApplicationState.Debug;
        public enum ApplicationState
        {
            Debug,
            Release
        }

        [SerializeField] private ApplicationState _mode = ApplicationState.Debug;

        
        private void Awake()
        {
            DebugMode = _mode;
        }
    }
}
