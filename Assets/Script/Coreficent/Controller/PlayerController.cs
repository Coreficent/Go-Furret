namespace Coreficent.Controller
{
    using Coreficent.Input;
    using Coreficent.Physics;
    using Coreficent.Utility;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private KeyboardInput _keyboardInput;
        [SerializeField] private Planet _planet;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _turnSpeed = 90.0f;
        [SerializeField] private float _walkSpeed = 1.0f;
        [SerializeField] private float _jumpSpeed = 100.0f;

        public float Speed = 0.0f;

        private Vector3 _velocity = new Vector3();

        // temp
        public GameObject LookObject;

        protected void Start()
        {
            SanityCheck.Check(this, _keyboardInput, _rigidbody, _planet);

            _rigidbody.useGravity = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && Landed)
            {
                _rigidbody.AddForce(transform.up * _jumpSpeed);
            }
        }
        protected void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Alpha2))
            {
                FaceEntity();
            }
            _planet.CalculatePhysics(gameObject);

            float turnSpeed = (-_keyboardInput.Left + _keyboardInput.Right);
            transform.Rotate(Vector3.up * turnSpeed * _turnSpeed * Time.fixedDeltaTime);

            float forwardSpeed = _keyboardInput.Forward;
            _velocity.z = forwardSpeed * _walkSpeed * Time.fixedDeltaTime;

            Speed = Mathf.Max(Mathf.Abs(forwardSpeed), Mathf.Abs(turnSpeed));

            _rigidbody.MovePosition(_rigidbody.position + transform.TransformDirection(_velocity));
        }

        public bool Landed
        {
            get { return Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, GetComponent<CapsuleCollider>().center.y, 0.0f)), -transform.up, GetComponent<CapsuleCollider>().height * 0.5f + 0.125f); }
        }

        public void FaceEntity()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookObject.transform.position - transform.position), _turnSpeed * 0.1f * Time.fixedDeltaTime);
        }
    }
}
