namespace Coreficent.Utility
{
    using Coreficent.Setting;
    using UnityEngine;

    public class DebugLogger
    {
        private static readonly string _delimiter = "::";
        private static readonly string _ender = ".";

        public static void Assert(string name, bool condition)
        {
            if (condition)
            {
                Log("Assert" + _delimiter + "Passed" + _delimiter + name);
            }
            else
            {
                Warn("Assert" + _delimiter + "Failed" + _delimiter + name);
            }
        }

        public static void Bug(params object[] message)
        {
            Output("Bug", message);
        }
        public static void ToDo(params object[] message)
        {
            Output("Todo", message);
        }
        public static void Start(params object[] message)
        {
            Initialize("Start", message);
        }
        public static void Initialize(string name, params object[] message)
        {
            Log("Initialized" + _delimiter + name, message);
        }
        public static void Log(params object[] message)
        {
            Output("Log", message);
        }
        public static void Warn(params object[] message)
        {
            Output("Warn", message);
        }

        public static void Error(params object[] message)
        {
            Output("Error", message);
        }

        private static void Output(string messageType, params object[] variables)
        {
            if (ApplicationMode.DebugMode == ApplicationMode.ApplicationState.Debug)
            {
                string message = "";

                foreach (object i in variables)
                {
                    message += i;
                    message += _delimiter;
                }

                switch (messageType)
                {
                    case "Error":
                        Debug.LogError(messageType + _delimiter + message + _ender);
                        break;

                    case "Warn":
                        Debug.LogWarning(messageType + _delimiter + message + _ender);
                        break;

                    case "Todo":
                        Debug.Log(messageType + _delimiter + message + _ender);
                        break;

                    default:
                        Debug.Log(messageType + _delimiter + message + _ender);
                        break;
                }
            }
        }
    }
}
