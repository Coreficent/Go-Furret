namespace Coreficent.Controller
{
    using Coreficent.Input;
    using Coreficent.Utility;
    using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        [SerializeField] private KeyboardInput _keyboardInput;
        [SerializeField] private GameObject _player;

        public float HorizontalOffset = -1.0f;
        public float VerticalOffset = 1.0f;
        public float RotationSpeed = 90.0f;

        private Vector3 _verticalVector = new Vector3();
        private Vector3 _horizontalVector = new Vector3();
        private Color _debugColor = Color.white;
        private float _radian = 0.0f;

        public Vector3 PositionDestination
        {
            get
            {
                _radian -= _keyboardInput.CameraLeft * RotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
                _radian += _keyboardInput.CameraRight * RotationSpeed * Mathf.Deg2Rad * Time.deltaTime;

                _verticalVector.y = VerticalOffset;
                Vector3 verticalPosition = _player.transform.position + _player.transform.TransformVector(_verticalVector);

                _horizontalVector.x = Mathf.Sin(_radian) * HorizontalOffset;
                _horizontalVector.z = Mathf.Cos(_radian) * HorizontalOffset;

                Vector3 horizontalPosition = _player.transform.TransformVector(_horizontalVector);

                DebugRender.Draw(_player.transform.position, verticalPosition, _debugColor);
                DebugRender.Draw(verticalPosition, verticalPosition + horizontalPosition, _debugColor);

                return verticalPosition + horizontalPosition;
            }
        }

        public Quaternion RotationDestination
        {
            get { return Quaternion.LookRotation((_player.transform.position - transform.position).normalized, _player.transform.position.normalized); }
        }

        protected void Start()
        {
            SanityCheck.Check(this, _player, _keyboardInput);

            enabled = false;
        }

        protected void Update()
        {
            UpdatePosition();
            UpdateRotation();
        }

        private void UpdatePosition()
        {
            transform.position = PositionDestination;
        }

        private void UpdateRotation()
        {
            transform.rotation = RotationDestination;
        }
    }
}

