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

        protected void Start()
        {
            SanityCheck.Check(this, _acceleration);

            _forward = new KeyState(_acceleration, KeyCode.W);
            _left = new KeyState(_acceleration, KeyCode.A);
            _right = new KeyState(_acceleration, KeyCode.D);
        }

        protected void Update()
        {
            _forward.Run();
            _left.Run();
            _right.Run();
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
    }
}

