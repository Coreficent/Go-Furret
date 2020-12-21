namespace Coreficent.Control
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        private Animator _animator;
        private Vector3 inputDirection;

        private readonly string _verticalControl = "Vertical";
        private readonly string _horizontalControl = "Horizontal";
        private readonly float radius = 500.0f;

        private float _walkSpeed = 500.0f;
        private float _rotationSpeed = 10.0f;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Update()
        {
            _animator.SetBool("Started", Input.GetKey(KeyCode.Alpha1));

            inputDirection = new Vector3(Input.GetAxis(_horizontalControl), Input.GetAxis(_verticalControl)).normalized;
            if (inputDirection.magnitude > 0.5f)
            {
                CalculatetRotation();
                CalculateMovement();
            }
            //ApplyPhysics();
        }

        private void CalculatetRotation()
        {
            var from = transform.position * -1.0f;


            //var q = Quaternion.AngleAxis(Vector3.Angle(new Vector3(0.0f, 0.0f, -500.0f), transform.position), new Vector3(0.0f, 0.0f, -500.0f));

            //var to = q * inputDirection;

            //var to = transform.InverseTransformDirection(inputDirection);

            //var to = inputDirection;
            //var to = transform.Find("guide").transform.InverseTransformVector(inputDirection);

            Quaternion q = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -500.0f), transform.position);

            var to = q * inputDirection;

            Debug.DrawLine(new Vector3(), from * 500.0f, Color.blue);
            Debug.DrawLine(new Vector3(), to * 500.0f, Color.red);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(from, to), _rotationSpeed * Time.deltaTime);
        }

        private void CalculateMovement()
        {
            //Vector3 direction = (transform.rotation * Vector3.up).normalized;
            Quaternion q = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -500.0f), transform.position);

            var to = q * inputDirection;
            transform.position += to.normalized * _walkSpeed * Time.deltaTime;
        }

        private void ApplyPhysics()
        {
            transform.position = transform.position.normalized * radius;
        }
    }
}
