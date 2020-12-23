namespace Coreficent.Physics
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Entity : MonoBehaviour
    {
        public GameObject Planet;

        private float _turnAmount = 250.0f;
        private float _walkSpeed = 250.0f;
        private Vector3 _inputDirection = new Vector3();


        private void Awake()
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }

        private void Update()
        {

        }

        private void FixedUpdate()
        {
            Planet.GetComponent<Planet>().CalculatePhysics(gameObject);

            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * _turnAmount * Time.fixedDeltaTime);

            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"))) * _walkSpeed * Time.fixedDeltaTime);
        }
    }
}
