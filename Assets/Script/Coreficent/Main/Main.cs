namespace Coreficent.Main
{
    using UnityEngine;
    using Coreficent.Utility;
    using Coreficent.Controller;
    using UnityEngine.UI;
    using Coreficent.Input;
    using Coreficent.Setting;

    public class Main : MonoBehaviour
    {
        public enum GameState
        {
            Menu,
            Transition,
            Initialization,
            Execution,
            Hint,
            Win
        }

        public GameState State = GameState.Menu;

        [SerializeField] private PlayerController _furret;
        [SerializeField] private CameraController _camera;
        [SerializeField] private Text _title;
        [SerializeField] private Text _control;
        [SerializeField] private Text _win;
        [SerializeField] private KeyboardInput _keyboardInput;

        private readonly TimeController _timeController = new TimeController();

        private Vector3 _initialCameraPosition = Vector3.zero;
        private Quaternion _initialCameraRotation = Quaternion.identity;
        private Color _initialTitleColor = Color.clear;
        private Color _initialControlColor = Color.clear;

        private bool _winComplete = false;

        protected void Start()
        {
            SanityCheck.Check(this, _furret, _camera, _title, _control, _keyboardInput);

            _initialCameraPosition = _camera.transform.position;
            _initialCameraRotation = _camera.transform.rotation;

            _initialTitleColor = _title.color;
            _initialControlColor = _control.color;

            if (ApplicationMode.DebugMode == ApplicationMode.ApplicationState.Debug)
            {
                _title.color = _initialTitleColor = Color.green;
            }
        }

        protected void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
            switch (State)
            {
                case GameState.Menu:
                    if (Input.anyKey)
                    {
                        GoTo(GameState.Transition);
                    }
                    break;

                case GameState.Transition:
                    float transitionTime = 12.5f;

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

                    if (_furret.FoundRainbowBean && !_winComplete)
                    {
                        GoTo(GameState.Win);
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

                case GameState.Win:
                    float appearTime = 3.0f;

                    _win.color = Color.Lerp(Color.clear, _initialTitleColor, _timeController.Progress(appearTime));

                    if (_timeController.Passed(appearTime))
                    {
                        GoTo(GameState.Execution);
                    }

                    _winComplete = true;

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
