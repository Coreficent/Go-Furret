namespace Coreficent.Controller
{
    using Coreficent.Physics;
    using Coreficent.Utility;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        public GameObject Planet;
        public float Speed = 0.0f;

        private Rigidbody _rigidbody = null;
        private Planet _planet = null;

        private Vector3 _velocity = new Vector3();

        private float _turnAmount = 250.0f;
        private float _walkSpeed = 2.5f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _planet = Planet.GetComponent<Planet>();

            SanityCheck.Check(this, Planet, _rigidbody, _planet);

            _rigidbody.useGravity = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        private void FixedUpdate()
        {
            _planet.CalculatePhysics(gameObject);

            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * _turnAmount * Time.fixedDeltaTime);

            Speed = Input.GetAxis("Vertical") * _walkSpeed;
            _velocity.z = Speed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + transform.TransformDirection(_velocity));
        }
    }
}
