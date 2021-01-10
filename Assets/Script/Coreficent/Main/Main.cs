namespace Coreficent.Main
{
    using UnityEngine;
    using Coreficent.Utility;
    using Coreficent.Controller;
    using UnityEngine.UI;
    using Coreficent.Input;

    public class Main : MonoBehaviour
    {
        public enum GameState
        {
            Menu,
            Transition,
            Initialization,
            Execution,
            Hint
        }

        public GameState State = GameState.Menu;

        [SerializeField] private PlayerController _furret;
        [SerializeField] private CameraController _camera;
        [SerializeField] private Text _title;
        [SerializeField] private Text _control;
        [SerializeField] private KeyboardInput _keyboardInput;

        private readonly TimeController _timeController = new TimeController();

        private Vector3 _initialCameraPosition = Vector3.zero;
        private Quaternion _initialCameraRotation = Quaternion.identity;
        private Color _initialTitleColor = Color.clear;
        private Color _initialControlColor = Color.clear;

        protected void Start()
        {
            SanityCheck.Check(this, _furret, _camera, _title, _control, _keyboardInput);

            _initialCameraPosition = _camera.transform.position;
            _initialCameraRotation = _camera.transform.rotation;

            _initialTitleColor = _title.color;
            _initialControlColor = _control.color;
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

                    _camera.transform.position = Vector3.Lerp(_initialCameraPosition, _camera.PositionDestination, _timeController.Progress(transitionTime));
                    _camera.transform.rotation = Quaternion.Lerp(_initialCameraRotation, _camera.RotationDestination, _timeController.Progress(transitionTime));

                    _title.color = Color.Lerp(_initialTitleColor, Color.clear, _timeController.Progress(transitionTime) * 2.0f);
                    _control.color = Color.Lerp(_initialControlColor, Color.clear, _timeController.Progress(transitionTime) * 2.0f);

                    if (_timeController.Passed(transitionTime))
                    {
                        GoTo(GameState.Initialization);
                    }
                    break;

                case GameState.Initialization:
                    _furret.GoTo(PlayerController.PlayerState.Stand);
                    _camera.enabled = true;
                    GoTo(GameState.Execution);
                    break;

                case GameState.Execution:
                    if (_keyboardInput.AnyKey && _keyboardInput.KeyInvalid)
                    {
                        GoTo(GameState.Hint);
                    }
                    break;

                case GameState.Hint:
                    float hintTime = 4.0f;

                    float stayTime = hintTime * 0.5f;

                    _control.color = Color.Lerp(Color.clear, _initialControlColor, stayTime - Mathf.Abs(_timeController.Progress(hintTime) * stayTime * 2.0f - stayTime));

                    if (_timeController.Passed(hintTime))
                    {
                        GoTo(GameState.Execution);
                    }
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
