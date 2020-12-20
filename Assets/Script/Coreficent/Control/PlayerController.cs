namespace Coreficent.Control
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Update()
        {
            _animator.SetBool("Started", Input.GetKey(KeyCode.S));
            transform.rotation = Quaternion.LookRotation(transform.position * -1.0f, Vector3.down);
        }
    }
}
