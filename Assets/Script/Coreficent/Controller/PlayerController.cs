namespace Coreficent.Controller
{
    using Coreficent.Physics;
    using Coreficent.Utility;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Planet _planet;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _turnSpeed = 90.0f;
        [SerializeField] private float _walkSpeed = 1.0f;
        [SerializeField] private float _jumpSpeed = 100.0f;

        private Vector3 _velocity = new Vector3();

        private float _speed = 0.0f;
        // use state machine instead.
        private bool _jumping = false;

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



            Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0, 1.50f, 0)), -transform.up);

            if (!_jumping && Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, GetComponent<CapsuleCollider>().center.y, 0.0f)), -transform.up, GetComponent<CapsuleCollider>().height))
            {
                DebugLogger.Log("true");
                if (Input.GetButtonDown("Jump"))
                {
                    _rigidbody.AddForce(transform.up * _jumpSpeed);
                    _jumping = true;
                }
            }
            else
            {
                _jumping = false;
                DebugLogger.Log("false");
            }
        }
    }
}
