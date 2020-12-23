namespace Coreficent.Controller
{
    using Coreficent.Utility;
    using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        public GameObject Furret;

        private Vector3 _offset = new Vector3(0.0f, 250.0f, 0.0f);

        private void Start()
        {
            SanityCheck.Check(this, Furret);
        }

        private void Update()
        {
            DebugRender.Draw(Furret.transform.position, Furret.transform.position + Furret.transform.TransformVector(_offset), Color.black);
            DebugRender.Draw(transform.position, Furret.transform.position + Furret.transform.TransformVector(_offset), Color.black);

            transform.rotation = Quaternion.LookRotation(Furret.transform.position - transform.position);
        }
    }
}

