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

        private Vector3 _velocity = new Vector3();

        private float _speed = 0.0f;

        // temp
        public GameObject LookObject;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        protected void Start()
        {
            SanityCheck.Check(this, _keyboardInput, _rigidbody, _planet);

            _rigidbody.useGravity = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
        protected void Update()
        {
            if (Input.GetButtonDown("Jump") && Landed)
            {
                _rigidbody.AddForce(transform.up * _jumpSpeed);
            }
        }

        public bool Landed
        {
            get { return Physics.Raycast(transform.position + transform.TransformDirection(new Vector3(0.0f, GetComponent<CapsuleCollider>().center.y, 0.0f)), -transform.up, GetComponent<CapsuleCollider>().height * 0.5f + 0.125f); }
        }

        public void FaceEntity()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookObject.transform.position - transform.position), _turnSpeed * 0.1f * Time.fixedDeltaTime);
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Alpha2))
            {
                FaceEntity();
            }
            _planet.CalculatePhysics(gameObject);

            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * _turnSpeed * Time.fixedDeltaTime);

            Speed = _keyboardInput.Forward * _walkSpeed;

            _velocity.z = Speed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + transform.TransformDirection(_velocity));
        }
    }
}
