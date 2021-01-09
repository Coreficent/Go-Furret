namespace Coreficent.Controller
{
    using Coreficent.Input;
    using Coreficent.Physics;
    using Coreficent.Utility;
    using Coreficent.Food;
    using UnityEngine;
    using Coreficent.Plant;

    public class PlayerController : MonoBehaviour
    {
        public enum PlayerState
        {
            Stay,
            Float,
            Stand,
            Walk,
            Run,
            Reject,
            Eat,
            Shake,
            Throw,
            Cook,
            Consume,
            Search,
            Sleep,
            Wake
        }

        [SerializeField] private KeyboardInput _keyboardInput;
        [SerializeField] private Planet _planet;
        [SerializeField] private Cooker _cooker;

        [SerializeField] private float _turnSpeed = 90.0f;
        [SerializeField] private float _walkSpeed = 1.0f;
        [SerializeField] private float _runSpeed = 5.0f;
        [SerializeField] private float _runAcceleration = 0.1f;
        [SerializeField] private float _jumpSpeed = 100.0f;
        [SerializeField] private float _boreTime = 10.0f;

        public PlayerState State = PlayerState.Float;

        public float Throttle = 0.0f;

        private Rigidbody _rigidbody;
        private CapsuleCollider _capsuleCollider;
        private RaycastHit _hitInfo;

        private Color _debugColor = new Color(0.85f, 0.7f, 0.5f, 1.0f);
        private Vector3 _landingPosition = Vector3.zero;
        private Vector3 _velocity = Vector3.zero;
        private readonly TimeController _timeController = new TimeController();

        protected void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();

            SanityCheck.Check(this, _keyboardInput, _planet, _cooker, _rigidbody, _capsuleCollider);
        }

        protected void FixedUpdate()
        {
            DebugLogger.Log("current state", State);

            //DebugLogger.Log("state animation state", _animator.GetCurrentAnimatorStateInfo(0).IsName("Reject"));


            PlayerState nextState = PlayerState.Stay;

            switch (State)
            {
                case PlayerState.Stay:
                    if (Land())
                    {
                        nextState = PlayerState.Stand;
                    }

                    break;

                case PlayerState.Float:
                    Turn();
                    Move(_walkSpeed);

                    if (Land())
                    {
                        nextState = PlayerState.Stand;
                    }

                    break;

                case PlayerState.Stand:
                    if (Turn())
                    {
                        nextState = PlayerState.Walk;
                    }
                    if (Move(_walkSpeed))
                    {
                        nextState = PlayerState.Walk;
                    }
                    if (Jump())
                    {
                        nextState = PlayerState.Float;
                    }
                    PlayerState inspectState = Inspect();

                    if (inspectState != PlayerState.Stay)
                    {
                        nextState = inspectState;

                        if (nextState == PlayerState.Eat)
                        {
                            _hitInfo.collider.gameObject.GetComponent<Fruit>().DisablePhysics();
                        }

                        DebugLogger.Log("inspect state", nextState);
                    }

                    if (_timeController.Passed(_boreTime))
                    {
                        nextState = PlayerState.Sleep;
                    }

                    break;

                case PlayerState.Walk:
                    bool turning = Turn();
                    bool moving = Move(_walkSpeed);

                    if (!turning && !moving)
                    {
                        nextState = PlayerState.Stand;
                    }

                    if (Run())
                    {
                        nextState = PlayerState.Run;
                    }

                    if (Jump())
                    {
                        nextState = PlayerState.Float;
                    }

                    if (_cooker.FruitVacuumSizeChanged())
                    {
                        DebugLogger.Log("throw item");
                        nextState = PlayerState.Throw;
                    }

                    break;

                case PlayerState.Run:
                    bool revolving = Turn();
                    bool running = Move(_runSpeed);

                    if (!running)
                    {
                        if (revolving)
                        {
                            nextState = PlayerState.Walk;
                        }
                        else
                        {
                            nextState = PlayerState.Stand;
                        }
                    }

                    break;

                case PlayerState.Reject:
                    nextState = Reject();

                    DebugLogger.Log("reject return", nextState);

                    break;

                case PlayerState.Eat:
                    nextState = Eat();

                    break;

                case PlayerState.Shake:
                    nextState = Shake();

                    break;

                case PlayerState.Throw:
                    Move(_walkSpeed);
                    nextState = Throw();

                    break;

                case PlayerState.Cook:
                    nextState = Cook();
                    break;

                case PlayerState.Consume:
                    nextState = Consume();
                    break;

                case PlayerState.Search:
                    nextState = Search();
                    break;

                case PlayerState.Sleep:
                    nextState = Sleep();
                    break;

                case PlayerState.Wake:
                    nextState = Wake();
                    break;

                default:
                    DebugLogger.Warn("unexpected player state");
                    break;
            }

            GoTo(nextState);
        }

        private void OnCollisionStay(Collision collision)
        {
            Fruit fruit = collision.gameObject.GetComponent<Fruit>();
            if (fruit)
            {
                Vector3 fruitDirection = Vector3.Normalize(collision.gameObject.transform.position - transform.position);

                Vector3 forceVector = Vector3.Normalize(transform.forward - fruitDirection) * 1.0f;

                DebugRender.Draw(transform.position, transform.position + forceVector, _debugColor);

                collision.gameObject.GetComponent<Rigidbody>().AddForce(forceVector);
            }
        }

        private void GoTo(PlayerState nextState)
        {
            if (nextState != PlayerState.Stay)
            {
                _timeController.Reset();
            }
            State = nextState == PlayerState.Stay ? State : nextState;
        }

        private bool Turn()
        {
            float turnSpeed = (-_keyboardInput.Left + _keyboardInput.Right);

            transform.Rotate(Vector3.up * turnSpeed * _turnSpeed * Time.fixedDeltaTime);

            return Mathf.Abs(turnSpeed) > 0.0f;
        }

        private bool Move(float speed)
        {
            float forwardThrottle = _keyboardInput.Forward;
            float previousSpeed = _velocity.magnitude;

            _velocity.z = forwardThrottle * speed;

            if (_velocity.magnitude > previousSpeed)
            {
                _velocity.z = previousSpeed + _runAcceleration;
            }

            _rigidbody.MovePosition(_rigidbody.position + transform.TransformDirection(_velocity * Time.fixedDeltaTime));

            return Mathf.Abs(forwardThrottle) > 0.0f;
        }
        private bool Run()
        {
            if (_keyboardInput.GetAction)
            {
                return true;
            }

            return false;
        }
        private bool Jump()
        {
            if (_keyboardInput.GetJump)
            {
                _rigidbody.AddForce(transform.up * _jumpSpeed);
                return true;
            }

            return false;
        }

        private bool Land()
        {
            _landingPosition.y = _capsuleCollider.center.y;

            Vector3 origin = transform.position + transform.TransformDirection(_landingPosition);
            Vector3 direction = -transform.up;
            float magnitude = _capsuleCollider.height * 0.5f + 0.125f;

            DebugRender.Draw(origin, origin + direction * magnitude, _debugColor);

            bool rayHit = Physics.Raycast(origin, direction, magnitude);

            bool movingUp = Vector3.Dot(Vector3.Normalize(_rigidbody.velocity), transform.up) >= 0.0f;

            return rayHit && !movingUp;
        }

        private PlayerState Inspect()
        {
            if (_keyboardInput.GetAction)
            {
                Vector3 origin = transform.position + transform.TransformDirection(_capsuleCollider.center) * 0.5f + transform.TransformDirection(Vector3.forward) * 0.5f;
                Vector3 direction = transform.TransformDirection(Vector3.forward);
                float magnitude = 1.0f;

                DebugRender.Draw(origin, origin + direction * magnitude, _debugColor);

                if (Physics.Raycast(origin, direction, out _hitInfo, magnitude))
                {
                    if (_hitInfo.collider.gameObject.GetComponent<Fruit>())
                    {
                        return PlayerState.Eat;
                    }
                    else if (_hitInfo.collider.gameObject.GetComponent<PokeTree>())
                    {
                        return PlayerState.Shake;
                    }
                    else if (_hitInfo.collider.gameObject.GetComponent<Cooker>())
                    {
                        if (_cooker.State == Cooker.CookerState.Vacuum)
                        {
                            if (_cooker.CanCook())
                            {
                                _cooker.Cook();
                                return PlayerState.Cook;
                            }
                            else
                            {
                                return PlayerState.Reject;
                            }
                        }
                        else if (_cooker.State == Cooker.CookerState.Serve)
                        {
                            return PlayerState.Consume;
                        }
                        else
                        {
                            DebugLogger.Bug("hit" + _hitInfo.collider.gameObject.name);

                            return PlayerState.Stay;
                        }
                    }
                    else
                    {
                        DebugLogger.Bug("hit no collider" + _hitInfo.collider.gameObject.name);
                        return PlayerState.Stay;
                    }
                }
                else
                {
                    return PlayerState.Search;
                }
            }
            else
            {
                DebugLogger.Bug("not hit");
                return PlayerState.Stay;
            }
        }

        private PlayerState Reject()
        {
            if (_timeController.Passed(2.0f))
            {
                return PlayerState.Stand;
            }

            return PlayerState.Stay;
        }

        private PlayerState Eat()
        {
            Fruit edible = _hitInfo.collider.gameObject.GetComponent<Fruit>();

            edible.Feed(_timeController.Progress(edible.ConsumeTime));

            if (_timeController.Passed(edible.ConsumeTime))
            {
                return PlayerState.Stand;
            }

            return PlayerState.Stay;
        }

        private PlayerState Shake()
        {
            if (_timeController.Passed(2.0f))
            {
                PokeTree pokeTree = _hitInfo.collider.gameObject.GetComponent<PokeTree>();

                pokeTree.SpawnFruitNear(gameObject);

                return PlayerState.Stand;
            }

            return PlayerState.Stay;
        }

        private PlayerState Throw()
        {
            if (_timeController.Passed(0.5f))
            {
                return PlayerState.Stand;
            }

            return PlayerState.Stay;
        }

        private PlayerState Cook()
        {
            DebugLogger.Log("player cooking", _cooker.RecipeTime);

            if (_timeController.Passed(_cooker.RecipeTime))
            {
                return PlayerState.Stand;
            }

            return PlayerState.Stay;
        }

        private PlayerState Consume()
        {
            Cooker cooker = _hitInfo.collider.gameObject.GetComponent<Cooker>();

            cooker.Bean.Feed(_timeController.Progress(cooker.Bean.ConsumeTime));

            if (_timeController.Passed(cooker.Bean.ConsumeTime))
            {
                if (cooker.Bean.Pattern == Bean.BeanPattern.Gray && cooker.Bean.Color == Fruit.Black)
                {
                    return PlayerState.Reject;
                }

                return PlayerState.Stand;
            }

            return PlayerState.Stay;
        }

        private PlayerState Search()
        {
            if (_timeController.Passed(2.0f))
            {
                return PlayerState.Stand;
            }

            return PlayerState.Stay;
        }

        private PlayerState Sleep()
        {
            float threshold = 0.5f;

            if (_keyboardInput.Forward > threshold || _keyboardInput.Left > threshold || _keyboardInput.Right > threshold || _keyboardInput.GetAction || _keyboardInput.GetJump)
            {
                return PlayerState.Wake;
            }

            return PlayerState.Stay;
        }

        private PlayerState Wake()
        {
            if (_timeController.Passed(3.0f))
            {
                return PlayerState.Stand;
            }

            return PlayerState.Stay;
        }
    }
}
