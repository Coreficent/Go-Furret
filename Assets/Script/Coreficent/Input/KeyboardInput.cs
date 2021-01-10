namespace Coreficent.Input
{
    using Coreficent.Utility;
    using UnityEngine;
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private float _acceleration;

        private KeyState _forward;
        private KeyState _left;
        private KeyState _right;
        private KeyState _cameraLeft;
        private KeyState _cameraRight;
        private KeyState _jump;
        private KeyState _action;

        protected void Start()
        {
            SanityCheck.Check(this, _acceleration);

            _forward = new KeyState(_acceleration, KeyCode.W);
            _left = new KeyState(_acceleration, KeyCode.A);
            _right = new KeyState(_acceleration, KeyCode.D);
            _cameraLeft = new KeyState(_acceleration, KeyCode.Q);
            _cameraRight = new KeyState(_acceleration, KeyCode.E);
            _jump = new KeyState(_acceleration, KeyCode.Space);
            _action = new KeyState(_acceleration, KeyCode.S);
        }

        protected void Update()
        {
            _forward.Run();
            _left.Run();
            _right.Run();
            _cameraLeft.Run();
            _cameraRight.Run();
        }

        public float Forward
        {
            get => _forward.Axis;
        }

        public float Left
        {
            get => _left.Axis;
        }

        public float Right
        {
            get => _right.Axis;
        }

        public float CameraLeft
        {
            get => _cameraLeft.Axis;
        }

        public float CameraRight
        {
            get => _cameraRight.Axis;
        }

        public bool GetJump
        {
            get => _jump.GetKey;
        }

        public bool GetAction
        {
            get => _action.GetKey;
        }

        public bool GetForward
        {
            get => _forward.GetKey;
        }

        public bool GetCameraLeft
        {
            get => _cameraLeft.GetKey;
        }
        public bool GetCameraRight
        {
            get => _cameraRight.GetKey;
        }
        public bool GetLeft
        {
            get => _left.GetKey;
        }
        public bool GetRight
        {
            get => _right.GetKey;
        }

        public bool KeyInvalid
        {
            get => !GetForward && !GetCameraLeft && !GetCameraRight && !GetLeft && !GetRight && !GetAction && !GetJump;
        }

        public bool AnyKey
        {
            get => Input.anyKey;
        }
    }
}

