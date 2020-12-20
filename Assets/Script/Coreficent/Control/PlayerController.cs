namespace Coreficent.Control
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerController
    {
        private GameObject _player;
        private Animator _animator;

        public PlayerController(GameObject player)
        {
            _player = player;
            _animator = _player.GetComponent<Animator>();
        }

        public void Run()
        {
            _animator.SetBool("Started", Input.GetKey(KeyCode.S));
            _player.transform.rotation = Quaternion.LookRotation(_player.transform.position * -1.0f);
        }
    }
}
