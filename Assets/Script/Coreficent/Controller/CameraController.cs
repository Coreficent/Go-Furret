﻿namespace Coreficent.Controller
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

        private float _radian = 0.0f;
        private Vector3 _verticalVector = new Vector3();
        private Vector3 _horizontalVector = new Vector3();

        protected void Start()
        {
            SanityCheck.Check(this, _player, _keyboardInput);
        }

        protected void Update()
        {
            UpdatePosition();
            UpdateRotation();
        }

        private void UpdatePosition()
        {
            _radian -= _keyboardInput.CameraLeft * RotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
            _radian += _keyboardInput.CameraRight * RotationSpeed * Mathf.Deg2Rad * Time.deltaTime;

            _verticalVector.y = VerticalOffset;
            Vector3 verticalPosition = _player.transform.position + _player.transform.TransformVector(_verticalVector);

            _horizontalVector.x = Mathf.Sin(_radian) * HorizontalOffset;
            _horizontalVector.z = Mathf.Cos(_radian) * HorizontalOffset;

            Vector3 horizontalPosition = _player.transform.TransformVector(_horizontalVector);

            DebugRender.Draw(_player.transform.position, verticalPosition, Color.black);
            DebugRender.Draw(verticalPosition, verticalPosition + horizontalPosition, Color.black);

            transform.position = verticalPosition + horizontalPosition;
        }

        private void UpdateRotation()
        {
            transform.rotation = Quaternion.LookRotation((_player.transform.position - transform.position).normalized, _player.transform.position.normalized);
        }
    }
}

