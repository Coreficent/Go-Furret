namespace Coreficent.Main
{
    using UnityEngine;
    using Coreficent.Utility;
    using Coreficent.Controller;

    public class Main : MonoBehaviour
    {
        public enum GameState
        {
            Menu,
            Transition,
            Initialization,
            Execution
        }

        public GameState State = GameState.Menu;

        public PlayerController Furret;
        public CameraController Camera;

        private readonly TimeController _timeController = new TimeController();

        private Vector3 _initialCameraPosition = Vector3.zero;
        private Quaternion _initialCameraRotation = Quaternion.identity;

        protected void Start()
        {
            SanityCheck.Check(this, Furret, Camera);

            _initialCameraPosition = Camera.transform.position;
            _initialCameraRotation = Camera.transform.rotation;
        }

        protected void Update()
        {
            switch (State)
            {
                case GameState.Menu:
                    if (Input.anyKey)
                    {
                        GoTo(GameState.Transition);
                    }
                    break;

                case GameState.Transition:
                    float transitionTime = 10.0f;

                    Camera.transform.position = Vector3.Lerp(_initialCameraPosition, Camera.PositionDestination, _timeController.Progress(transitionTime));
                    Camera.transform.rotation = Quaternion.Lerp(_initialCameraRotation, Camera.RotationDestination, _timeController.Progress(transitionTime));

                    if (_timeController.Passed(transitionTime))
                    {
                        GoTo(GameState.Initialization);
                    }
                    break;

                case GameState.Initialization:
                    Furret.GoTo(PlayerController.PlayerState.Stand);
                    Camera.enabled = true;
                    GoTo(GameState.Execution);
                    break;

                case GameState.Execution:

                    break;

                default:
                    DebugLogger.Warn("unexpected game state");
                    break;
            }
        }

        private void GoTo(GameState nextState)
        {
            _timeController.Reset();
            State = nextState;
        }
    }
}
