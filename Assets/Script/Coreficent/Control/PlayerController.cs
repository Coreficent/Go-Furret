namespace Coreficent.Control
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        private readonly string _verticalControl = "Vertical";
        private readonly string _horizontalControl = "Horizontal";
        private float _walkSpeed = 50.0f;
        private float _rotationSpeed = 50.0f;
        private Animator _animator;


        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Update()
        {
            _animator.SetBool("Started", Input.GetKey(KeyCode.S));

            Vector3 inputDirection = new Vector3(Input.GetAxis(_horizontalControl), Input.GetAxis(_verticalControl));

            if(inputDirection.magnitude > 0.5f)
            {
                transform.rotation = Quaternion.LookRotation(transform.position * -1.0f, inputDirection.normalized);
            }
        }
    }
}
