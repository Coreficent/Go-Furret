namespace Coreficent.Controller
{
    using Coreficent.Utility;
    using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        public GameObject Player;

        private Vector3 _verticalOffset = new Vector3(0.0f, 250.0f, 0.0f);
        private float _maximumDistance = 500.0f;
        private float _minimumDistance = 150.0f;
        private float _movementDistance = 10.0f;

        private void Start()
        {
            SanityCheck.Check(this, Player);
        }

        private void Update()
        {
            Vector3 playerTop = Player.transform.position + Player.transform.TransformVector(_verticalOffset);

            DebugRender.Draw(Player.transform.position, playerTop, Color.black);
            DebugRender.Draw(transform.position, playerTop, Color.black);

            Vector3 distanceTop = playerTop - transform.position;

            if (distanceTop.magnitude > _maximumDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTop, _movementDistance); ;
            }

            Vector3 distancePlayer = Player.transform.position - transform.position;

            if (distancePlayer.magnitude < _minimumDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTop * 2.0f, _movementDistance);
            }

            transform.rotation = Quaternion.LookRotation(Player.transform.position - transform.position, playerTop);
        }
    }
}

