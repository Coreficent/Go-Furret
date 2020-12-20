namespace Coreficent.Control
{
    using Coreficent.Utility;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        public GameObject Player;

        private Vector3 _distance = new Vector3(0.0f, -150.0f, -150.0f);

        private void Start()
        {
            SanityCheck.Check(this, Player);
        }
        private void Update()
        {
            transform.position = Player.transform.position + _distance;
            transform.rotation = Quaternion.LookRotation(Player.transform.position - transform.position);
        }
    }
}
