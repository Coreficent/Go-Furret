namespace Coreficent.Utility
{
    using UnityEngine;

    public class DebugLogger
    {
        private static readonly string _delimiter = "::";
        private static readonly string _ender = ".";

        public static void ToDo(object message)
        {
            Output("Todo", message);
        }
        public static void Awake(object message)
        {
            Initialize("Awake", message);
        }
        public static void Start(object message)
        {
            Initialize("Start", message);
        }
        public static void Initialize(string name, object message)
        {
            Log("Initialized" + _delimiter + name, message);
        }
        public static void Log(string name, object message)
        {
            Log(name + _delimiter + message);
        }
        public static void Log(object message)
        {
            Output("Log", message);
        }
        public static void Warn(object message)
        {
            Output("Warn", message);
        }

        public static void Error(object message)
        {
            Output("Error", message);
        }

        private static void Output(string messageType, object message)
        {
            if (ApplicationMode.DebugMode == ApplicationMode.ApplicationState.Debug)
            {
                switch (messageType)
                {
                    case "Error":
                        Debug.LogError(messageType + _delimiter + message + _ender);
                        break;

                    case "Warn":
                        Debug.LogWarning(messageType + _delimiter + message + _ender);
                        break;

                    default:
                        Debug.Log(messageType + _delimiter + message + _ender);
                        break;
                }
            }
        }
    }
}
