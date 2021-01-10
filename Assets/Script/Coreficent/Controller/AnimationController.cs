namespace Coreficent.Controller
{
    using Coreficent.Utility;
    using UnityEngine;

    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Material _eye;
        [SerializeField] private Material _mouth;

        private readonly string _expressionX = "_ExpressionX";
        private readonly string _expressionY = "_ExpressionY";

        protected void Start()
        {
            SanityCheck.Check(this, _playerController, _playerAnimator, _eye, _mouth);

            DebugLogger.Start(this);
        }

        protected void Update()
        {
            _playerAnimator.SetBool("Walking", _playerController.State == PlayerController.PlayerState.Walk);
            _playerAnimator.SetBool("Running", _playerController.State == PlayerController.PlayerState.Run);
            _playerAnimator.SetBool("Jumping", _playerController.State == PlayerController.PlayerState.Float);
            _playerAnimator.SetBool("Rejecting", _playerController.State == PlayerController.PlayerState.Reject);
            _playerAnimator.SetBool("Eating", _playerController.State == PlayerController.PlayerState.Eat);
            _playerAnimator.SetBool("Shaking", _playerController.State == PlayerController.PlayerState.Shake);
            _playerAnimator.SetBool("Throwing", _playerController.State == PlayerController.PlayerState.Throw);
            _playerAnimator.SetBool("Cooking", _playerController.State == PlayerController.PlayerState.Cook);
            _playerAnimator.SetBool("Consuming", _playerController.State == PlayerController.PlayerState.Consume);
            _playerAnimator.SetBool("Searching", _playerController.State == PlayerController.PlayerState.Search);
            _playerAnimator.SetBool("Sleeping", _playerController.State == PlayerController.PlayerState.Sleep);
            _playerAnimator.SetBool("Exclaiming", _playerController.State == PlayerController.PlayerState.Exclaim);
            _playerAnimator.SetBool("Delighting", _playerController.State == PlayerController.PlayerState.Delight);
        }

        public void UpdateExpression(AnimationEvent animationEvent)
        {
            string[] data = animationEvent.stringParameter.Split(' ');

            if (data.Length != 2)
            {
                DebugLogger.Warn("unexpected number of parameters in animation controller");
            }

            string[] eye = data[0].Split(':');

            if (eye.Length != 2)
            {
                DebugLogger.Warn("unexpected number of eye parameters in animation controller");
            }
            if (eye[0] != "Eye")
            {
                DebugLogger.Warn("unexpected eye data format");
            }

            string[] mouth = data[1].Split(':');

            if (mouth.Length != 2)
            {
                DebugLogger.Warn("unexpected number of mouth parameters in animation controller");
            }

            if (mouth[0] != "Mouth")
            {
                DebugLogger.Warn("unexpected mouth data format");
            }

            switch (eye[1])
            {
                case "Open":
                    _eye.SetFloat(_expressionX, 0.0f);
                    _eye.SetFloat(_expressionY, 3.0f);
                    break;
                case "Semi":
                    _eye.SetFloat(_expressionX, 0.0f);
                    _eye.SetFloat(_expressionY, 2.0f);
                    break;
                case "Closed":
                    _eye.SetFloat(_expressionX, 0.0f);
                    _eye.SetFloat(_expressionY, 1.0f);
                    break;
                case "Wincing":
                    _eye.SetFloat(_expressionX, 0.0f);
                    _eye.SetFloat(_expressionY, 0.0f);
                    break;
                case "Angry":
                    _eye.SetFloat(_expressionX, 1.0f);
                    _eye.SetFloat(_expressionY, 3.0f);
                    break;
                case "Happy":
                    _eye.SetFloat(_expressionX, 1.0f);
                    _eye.SetFloat(_expressionY, 2.0f);
                    break;
                case "Sad":
                    _eye.SetFloat(_expressionX, 1.0f);
                    _eye.SetFloat(_expressionY, 1.0f);
                    break;
                default:
                    DebugLogger.Warn("unexpected eye state");
                    break;
            }

            switch (mouth[1])
            {
                case "Closed":
                    _mouth.SetFloat(_expressionX, 0.0f);
                    _mouth.SetFloat(_expressionY, 3.0f);
                    break;
                case "Semi":
                    _mouth.SetFloat(_expressionX, 0.0f);
                    _mouth.SetFloat(_expressionY, 2.0f);
                    break;
                case "Open":
                    _mouth.SetFloat(_expressionX, 0.0f);
                    _mouth.SetFloat(_expressionY, 1.0f);
                    break;
                case "Tense":
                    _mouth.SetFloat(_expressionX, 0.0f);
                    _mouth.SetFloat(_expressionY, 0.0f);
                    break;
                case "Exclaim":
                    _mouth.SetFloat(_expressionX, 1.0f);
                    _mouth.SetFloat(_expressionY, 3.0f);
                    break;
                case "Angry":
                    _mouth.SetFloat(_expressionX, 1.0f);
                    _mouth.SetFloat(_expressionY, 2.0f);
                    break;
                default:
                    DebugLogger.Warn("unexpected mouth state");
                    break;
            }
        }
    }
}
