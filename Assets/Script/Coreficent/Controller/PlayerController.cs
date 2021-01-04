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


        [SerializeField] private float _turnSpeed = 90.0f;
        [SerializeField] private float _walkSpeed = 1.0f;
        [SerializeField] private float _jumpSpeed = 100.0f;

        public float Speed = 0.0f;

        private Vector3 _velocity = new Vector3();

        private Rigidbody _rigidbody;
        private CapsuleCollider _capsuleCollider;

        private Color _debugColor = new Color(0.85f, 0.7f, 0.5f, 1.0f);
        private Vector3 _landingPosition = new Vector3();
        private Vector3 _facingPosition = new Vector3();

        private RaycastHit hitInfo = new RaycastHit();

        // temp
        public GameObject LookObject;

        public bool Landed
        {
            get
            {
                _landingPosition.y = _capsuleCollider.center.y;

                Vector3 origin = transform.position + transform.TransformDirection(_landingPosition);
                Vector3 direction = -transform.up;
                float magnitude = _capsuleCollider.height * 0.5f + 0.125f;

                DebugRender.Draw(origin, origin + direction * magnitude, _debugColor);

                return Physics.Raycast(origin, direction, magnitude);
            }
        }

        public bool FacingFood
        {
            get
            {
                _facingPosition.y = _capsuleCollider.center.y;

                Vector3 origin = transform.position + transform.TransformDirection(_facingPosition);
                Vector3 direction = transform.TransformDirection(Vector3.forward);
                float magnitude = 1.0f;

                DebugRender.Draw(origin, origin + direction * magnitude, _debugColor);

                return Physics.Raycast(origin, direction, out hitInfo, magnitude);
            }
        }

        protected void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();

            SanityCheck.Check(this, _keyboardInput, _planet, _rigidbody, _capsuleCollider);

            _planet.Characters.Add(gameObject);
        }

        protected void Update()
        {
            if (_keyboardInput.JumpIsDown && Landed)
            {
                _rigidbody.AddForce(transform.up * _jumpSpeed);
            }

            if (_keyboardInput.GetAction)
            {
                if (FacingFood)
                {
                    Debug.Log("Facing food" + hitInfo.transform.gameObject);
                }
                else
                {

                }
            }

        }

        protected void FixedUpdate()
        {
            float turnSpeed = (-_keyboardInput.Left + _keyboardInput.Right);
            transform.Rotate(Vector3.up * turnSpeed * _turnSpeed * Time.fixedDeltaTime);

            float forwardSpeed = _keyboardInput.Forward;
            _velocity.z = forwardSpeed * _walkSpeed * Time.fixedDeltaTime;

            Speed = Mathf.Max(Mathf.Abs(forwardSpeed), Mathf.Abs(turnSpeed));

            _rigidbody.MovePosition(_rigidbody.position + transform.TransformDirection(_velocity));
        }

        protected void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject.name);
        }

        protected void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log(other.gameObject.name + "An object is still inside of the trigger");
            //if (_keyboardInput.GetAction)
            //{
            //FaceEntity();
            //}
        }

        public void FaceEntity()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookObject.transform.position - transform.position), _turnSpeed * 0.1f * Time.fixedDeltaTime);
        }
    }
}
