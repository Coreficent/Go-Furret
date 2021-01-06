namespace Coreficent.Controller
{
    using Coreficent.Utility;
    using UnityEngine;

    public class TimeController
    {
        private float _time = 0.0f;

        public float TimePassed
        {
            get { return Time.time - _time; }
        }

        public TimeController() { }

        public void Reset()
        {
            _time = Time.time;
            DebugLogger.Log("time reset");
        }

        public bool Passed(float time)
        {
            return TimePassed > time;
        }

        public float Progress(float time)
        {
            return TimePassed / time;
        }
    }
}
