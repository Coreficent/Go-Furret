namespace Coreficent.Input
{
    using Coreficent.Utility;
    using UnityEngine;
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private float _acceleration;

        private float _forward = 0.0f;

        protected void Start()
        {
            SanityCheck.Check(this, _acceleration);
        }

        public float Forward
        {
            get
            {
                if (_forward < 1.0f)
                {
                    _forward += _acceleration;
                }
                else
                {
                    _forward = 1.0f;
                }
                return _forward;
            }
        }
    }
}

