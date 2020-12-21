namespace Coreficent.Physics
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Entity : MonoBehaviour
    {
        public GameObject Planet;

        private float _turnAmount = 50.0f;

        private void Awake()
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }

        private void Update()
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime * _turnAmount);
        }

        private void FixedUpdate()
        {
            Planet.GetComponent<Planet>().CalculatePhysics(gameObject);
        }
    }
}
