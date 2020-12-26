namespace Coreficent.Controller
{
    using Coreficent.Utility;
    using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        public GameObject Player;
        public float OffsetX = 1.0f;
        public float OffsetY = -1.0f;

        private void Start()
        {
            SanityCheck.Check(this, Player);
        }

        private void Update()
        {
            Transform playerTransform = Player.transform;
            Vector3 playerPosition = playerTransform.position;

            Vector3 verticalPosition = playerPosition + playerTransform.TransformVector(new Vector3(0.0f, 2.0f, 0.0f));
            Vector3 horizontalPosition = playerTransform.TransformVector(new Vector3(0.0f, 0.0f, -2.0f));

            DebugRender.Draw(playerTransform.position, verticalPosition, Color.black);
            DebugRender.Draw(verticalPosition, verticalPosition + horizontalPosition, Color.black);

            transform.position = verticalPosition + horizontalPosition;
            transform.rotation = Quaternion.LookRotation(playerTransform.position - transform.position, verticalPosition);
        }
    }
}

