namespace Coreficent.Controller
{
    using Coreficent.Utility;
    using System.Collections.Generic;
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

        public void UpdateExpression(AnimationEvent animationEvent)
        {
            /*
            eye:

            open
            semi
            closed
            wincing
            angry
            happy
            sad

            mouth:

            closed
            semi
            open
            tense
            happy
            angry
             */
            JsonUtility.FromJson<Dictionary<string, string>>("");


            string[] data = animationEvent.stringParameter.Split(' ');

            if (data.Length != 2)
            {
                DebugLogger.Warn("unexpected number of parameters in animation controller");
            }

            string[] eye = data[0].Split(':');
            if (eye[0] != "Eye")
            {
                DebugLogger.Warn("unexpected eye data format");
            }


            string[] mouth = data[1].Split(':');
            if (mouth[0] != "Mouth")
            {
                DebugLogger.Warn("unexpected mouth data format");
            }



            Debug.Log("event happened");

            Debug.Log(animationEvent.floatParameter);
            Debug.Log(animationEvent.stringParameter);
            Debug.Log(animationEvent.intParameter);
        }
    }
}
