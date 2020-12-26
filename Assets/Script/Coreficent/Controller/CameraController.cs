namespace Coreficent.Controller
{
    using Coreficent.Utility;
    using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        public GameObject Player;

        private Vector3 _verticalOffset = new Vector3(0.0f, 2.0f, 0.0f);
        private float _maximumDistance = 5.0f;
        private float _minimumDistance = 1.0f;
        private float _movementDistance = 1.0f;

        private void Start()
        {
            SanityCheck.Check(this, Player);
        }

        private void Update()
        {
            Vector3 verticalPosition = Player.transform.position + Player.transform.TransformVector(_verticalOffset);
            Vector3 horizontalPosition = Player.transform.TransformVector(new Vector3(0.0f, 0.0f, -2.0f));

            DebugRender.Draw(Player.transform.position, verticalPosition, Color.black);
            DebugRender.Draw(verticalPosition, verticalPosition + horizontalPosition, Color.black);

            transform.position = verticalPosition + horizontalPosition;
            transform.rotation = Quaternion.LookRotation(Player.transform.position - transform.position, verticalPosition);
        }
    }
}

