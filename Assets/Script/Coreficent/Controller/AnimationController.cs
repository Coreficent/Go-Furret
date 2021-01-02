namespace Coreficent.Controller
{
    using Coreficent.Utility;
    using UnityEngine;

    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Animator _playerAnimator;

        private void Start()
        {
            SanityCheck.Check(this, _playerController, _playerAnimator);
        }

        void Update()
        {
            _playerAnimator.SetFloat("Speed", _playerController.Speed);
            _playerAnimator.SetBool("Jumping", !_playerController.Landed);
        }
    }
}
