namespace Coreficent.Input
{
    using Coreficent.Utility;
    using UnityEngine;
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private float _acceleration;

        private KeyState _forward;

        protected void Start()
        {
            SanityCheck.Check(this, _acceleration);

            _forward = new KeyState(_acceleration, KeyCode.W);
        }

        protected void Update()
        {
            _forward.Run();
        }

        public float Forward
        {
            get { return _forward.Axis; }
        }
    }
}

