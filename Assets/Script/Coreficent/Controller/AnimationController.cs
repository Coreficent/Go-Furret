namespace Coreficent.Controller
{
    using Coreficent.Utility;
    using UnityEngine;

    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Animator _playerAnimator;

        protected void Start()
        {
            SanityCheck.Check(this, _playerController, _playerAnimator);
        }

        protected void Update()
        {
            _playerAnimator.SetBool("Moving", _playerController.Speed > 0.5); ;
            _playerAnimator.SetBool("Jumping", !_playerController.Landed);
        }
    }
}
