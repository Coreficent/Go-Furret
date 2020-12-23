namespace Coreficent.Physics
{
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        public GameObject Planet;

        private float _turnAmount = 250.0f;
        private float _walkSpeed = 250.0f;

        private void Awake()
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }

        private void FixedUpdate()
        {
            Planet.GetComponent<Planet>().CalculatePhysics(gameObject);

            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * _turnAmount * Time.fixedDeltaTime);

            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"))) * _walkSpeed * Time.fixedDeltaTime);
        }
    }
}
