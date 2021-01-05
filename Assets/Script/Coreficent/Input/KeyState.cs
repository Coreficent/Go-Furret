namespace Coreficent.Input
{
    using UnityEngine;
    internal class KeyState
    {
        private readonly KeyCode _keyCode;
        private readonly float _acceleration;

        private readonly float _maximum = 1.0f;
        private readonly float _minimum = 0.0f;
        private float _axis = 0.0f;

        internal KeyState(float acceleration, KeyCode keyCode)
        {
            _acceleration = acceleration;
            _keyCode = keyCode;
        }

        internal void Run()
        {
            if (Input.GetKey(_keyCode))
            {
                if (_axis < _maximum)
                {
                    _axis += _acceleration;
                }
                else
                {
                    _axis = _maximum;
                }
            }
            else
            {
                if (_axis > _minimum)
                {
                    _axis -= _acceleration;
                }
                else
                {
                    _axis = _minimum;
                }
            }
        }

        internal float Axis
        {
            get { return _axis; }
        }

        internal bool GetKey
        {
            get { return Input.GetKey(_keyCode); }
        }
    }
}