namespace Coreficent.Controller
{
    using Coreficent.Utility;
    using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        public GameObject Furret;
        private void Start()
        {
            SanityCheck.Check(this, Furret);
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(Furret.transform.position - transform.position);
        }
    }
}

