namespace Coreficent.Control
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Controller
    {
        private Animator _animator;

        public Controller(Animator animator)
        {
            _animator = animator;
        }

        public void Run()
        {
            _animator.SetBool("Started", Input.GetKey(KeyCode.S));
        }
    }
}
