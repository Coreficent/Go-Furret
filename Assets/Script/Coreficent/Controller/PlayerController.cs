namespace Coreficent.Controller
{
    using Coreficent.Physics;
    using Coreficent.Utility;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _turnSpeed = 180.0f;
        [SerializeField] private float _walkSpeed = 5.0f;
        [SerializeField] private Planet _planet;
        [SerializeField] private Rigidbody _rigidbody;

        private Vector3 _velocity = new Vector3();

        private float _speed = 0.0f;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        private void Start()
        {
            SanityCheck.Check(this, _rigidbody, _planet);

            _rigidbody.useGravity = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        private void FixedUpdate()
        {
            _planet.CalculatePhysics(gameObject);

            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * _turnSpeed * Time.fixedDeltaTime);

            Speed = Input.GetAxis("Vertical") * _walkSpeed;

            _velocity.z = Speed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + transform.TransformDirection(_velocity));
        }
    }
}
