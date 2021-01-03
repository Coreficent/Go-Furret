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

        protected void Start()
        {
            SanityCheck.Check(this, _acceleration);

            _forward = new KeyState(_acceleration, KeyCode.W);
            _left = new KeyState(_acceleration, KeyCode.A);
            _right = new KeyState(_acceleration, KeyCode.D);
            _cameraLeft = new KeyState(_acceleration, KeyCode.Q);
            _cameraRight = new KeyState(_acceleration, KeyCode.E);
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
            get { return _forward.Axis; }
        }
        public float Left
        {
            get { return _left.Axis; }
        }
        public float Right
        {
            get { return _right.Axis; }
        }
        public float CameraLeft
        {
            get { return _cameraLeft.Axis; }
        }
        public float CameraRight
        {
            get { return _cameraRight.Axis; }
        }
    }
}

