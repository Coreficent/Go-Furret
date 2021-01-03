namespace Coreficent.Controller
{
    using Coreficent.Utility;
    using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        public GameObject Player;
        public float HorizontalOffset = -1.0f;
        public float VerticalOffset = 1.0f;
        public float RotationSpeed = 5.0f;

        private float _radian = 0.0f;
        private Vector3 _verticalVector = new Vector3();
        private Vector3 _horizontalVector = new Vector3();

        protected void Start()
        {
            SanityCheck.Check(this, Player);
        }

        protected void Update()
        {
            UpdatePosition();
            UpdateRotation();
        }

        private void UpdatePosition()
        {
            _radian -= Input.GetKey(KeyCode.Q) ? RotationSpeed * Mathf.Deg2Rad : 0.0f * Time.deltaTime;
            _radian += Input.GetKey(KeyCode.E) ? RotationSpeed * Mathf.Deg2Rad : 0.0f * Time.deltaTime;
            _verticalVector.y = VerticalOffset;
            Vector3 verticalPosition = Player.transform.position + Player.transform.TransformVector(_verticalVector);

            _horizontalVector.x = Mathf.Sin(_radian) * HorizontalOffset;
            _horizontalVector.z = Mathf.Cos(_radian) * HorizontalOffset;
            Vector3 horizontalPosition = Player.transform.TransformVector(_horizontalVector);

            DebugRender.Draw(Player.transform.position, verticalPosition, Color.black);
            DebugRender.Draw(verticalPosition, verticalPosition + horizontalPosition, Color.black);

            transform.position = verticalPosition + horizontalPosition;
        }

        private void UpdateRotation()
        {
            transform.rotation = Quaternion.LookRotation((Player.transform.position - transform.position).normalized, Player.transform.position.normalized);
        }
    }
}

