namespace Coreficent.Control
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerController
    {
        private Animator _animator;

        public PlayerController(Animator animator)
        {
            _animator = animator;
        }

        public void Run()
        {
            _animator.SetBool("Started", Input.GetKey(KeyCode.S));
        }
    }
}
