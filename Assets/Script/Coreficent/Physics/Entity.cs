namespace Coreficent.Physics
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Entity : MonoBehaviour
    {
        public GameObject Planet;
        private void Awake()
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }

        private void FixedUpdate()
        {
            Planet.GetComponent<Planet>().CalculatePhysics(gameObject);
        }
    }
}
