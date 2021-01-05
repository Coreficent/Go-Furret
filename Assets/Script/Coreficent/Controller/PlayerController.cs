﻿namespace Coreficent.Controller
{
    using Coreficent.Input;
    using Coreficent.Physics;
    using Coreficent.Utility;
    using Coreficent.Food;
    using UnityEngine;
    using Coreficent.Plant;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private KeyboardInput _keyboardInput;
        [SerializeField] private Planet _planet;

        [SerializeField] private float _turnSpeed = 90.0f;
        [SerializeField] private float _walkSpeed = 1.0f;
        [SerializeField] private float _jumpSpeed = 100.0f;


        public enum PlayerState
        {
            Stay,
            Float,
            Stand,
            Move,
            Reject,
            Eat,
            Shake
        }

        public PlayerState State = PlayerState.Float;

        public float Throttle = 0.0f;

        private Rigidbody _rigidbody;
        private CapsuleCollider _capsuleCollider;
        private RaycastHit _hitInfo;

        private Color _debugColor = new Color(0.85f, 0.7f, 0.5f, 1.0f);
        private Vector3 _landingPosition = new Vector3();
        private Vector3 _velocity = new Vector3();

        private float _time = 0.0f;

        protected void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();

            SanityCheck.Check(this, _keyboardInput, _planet, _rigidbody, _capsuleCollider);

            _planet.Characters.Add(gameObject);
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
                    Move();

                    if (Land())
                    {
                        nextState = PlayerState.Stand;
                    }

                    break;

                case PlayerState.Stand:

                    if (Inspect() != PlayerState.Stay)
                    {
                        nextState = Inspect();
                    }
                    if (Turn())
                    {
                        nextState = PlayerState.Move;
                    }
                    if (Move())
                    {
                        nextState = PlayerState.Move;
                    }
                    if (Jump())
                    {
                        nextState = PlayerState.Float;
                    }

                    DebugLogger.Log("eat state", nextState);

                    break;

                case PlayerState.Move:
                    bool turning = Turn();
                    bool moving = Move();

                    if (!turning && !moving)
                    {
                        nextState = PlayerState.Stand;
                    }

                    if (Jump())
                    {
                        nextState = PlayerState.Float;
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

                //DebugRender.Draw(collision.gameObject.transform.position, collision.gameObject.transform.position + forceVector * 5.0f, _debugColor);

                collision.gameObject.GetComponent<Rigidbody>().AddForce(forceVector);
            }
        }

        private void GoTo(PlayerState nextState)
        {
            _time = nextState == PlayerState.Stay ? _time : Time.time;
            State = nextState == PlayerState.Stay ? State : nextState;
        }

        private bool Turn()
        {
            float turnSpeed = (-_keyboardInput.Left + _keyboardInput.Right);

            transform.Rotate(Vector3.up * turnSpeed * _turnSpeed * Time.fixedDeltaTime);

            return Mathf.Abs(turnSpeed) > 0.0f;
        }

        private bool Move()
        {
            float forwardSpeed = _keyboardInput.Forward;

            _velocity.z = forwardSpeed * _walkSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + transform.TransformDirection(_velocity));

            return Mathf.Abs(forwardSpeed) > 0.0f;
        }

        private bool Jump()
        {
            if (_keyboardInput.JumpIsDown)
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

            //DebugLogger.Log("state ray", rayHit);
            //DebugLogger.Log("state up", movingUp);

            return rayHit && !movingUp;
        }

        private PlayerState Inspect()
        {
            if (_keyboardInput.GetAction)
            {
                Vector3 origin = transform.position + transform.TransformDirection(_capsuleCollider.center);
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
                    else
                    {
                        return PlayerState.Stay;
                    }
                }
                else
                {
                    return PlayerState.Reject;
                }
            }
            else
            {
                return PlayerState.Stay;
            }
        }

        private PlayerState Reject()
        {
            if (Time.time - _time > 2.0f)
            {
                return PlayerState.Stand;
            }

            return PlayerState.Stay;
        }

        private PlayerState Eat()
        {
            float timePassed = Time.time - _time;

            Fruit fruit = _hitInfo.collider.gameObject.GetComponent<Fruit>();

            if (Time.time - _time > 5.0f)
            {
                return PlayerState.Stand;
            }
            else if (timePassed > 4.0f)
            {
                fruit.DisplayState(4);
            }
            else if (timePassed > 3.0f)
            {
                fruit.DisplayState(3);
            }
            else if (timePassed > 2.0f)
            {
                fruit.DisplayState(2);
            }
            else if (timePassed > 1.0f)
            {
                fruit.DisplayState(1);
            }

            return PlayerState.Stay;
        }

        private PlayerState Shake()
        {
            if (Time.time - _time > 2.0f)
            {
                return PlayerState.Stand;
            }

            return PlayerState.Stay;
        }
    }
}
