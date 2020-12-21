namespace Coreficent.Physics
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class Planet : MonoBehaviour
    {
        public float Gravity = -50.0f;

        private void Awake()
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        public void CalculatePhysics(GameObject entity)
        {
            Vector3 to = (entity.transform.position - transform.position).normalized;
            entity.transform.rotation = Quaternion.FromToRotation(entity.transform.up.normalized, to) * entity.transform.rotation;
            entity.GetComponent<Rigidbody>().AddForce(to * Gravity);
        }
    }
}

