namespace Coreficent.Control
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class FurretControl
    {
        private Animator _furretAnimator;

        public FurretControl(Animator furretAnimator)
        {
            _furretAnimator = furretAnimator;
        }

        public void Run()
        {
            _furretAnimator.SetBool("Started", Input.GetKey(KeyCode.S));
        }
    }
}
