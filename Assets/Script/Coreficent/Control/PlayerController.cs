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

            inputDirection = new Vector2(Input.GetAxis(_horizontalControl), Input.GetAxis(_verticalControl)).normalized;
            if (inputDirection.magnitude > 0.5f)
            {
                CalculatetRotation();
                CalculateMovement();
            }
            ApplyPhysics();
        }

        private void CalculatetRotation()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position * -1.0f, inputDirection), _rotationSpeed * Time.deltaTime);
        }

        private void CalculateMovement()
        {
            Vector3 direction = (transform.rotation * Vector3.up).normalized;
            transform.position += direction * _walkSpeed * Time.deltaTime;
        }

        private void ApplyPhysics()
        {
            transform.position = transform.position.normalized * radius;
        }
    }
}
