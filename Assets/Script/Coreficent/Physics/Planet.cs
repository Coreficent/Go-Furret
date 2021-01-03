namespace Coreficent.Physics
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Planet : KinematicObject
    {
        public List<GameObject> Characters = new List<GameObject>();
        public List<GameObject> Entities = new List<GameObject>();

        public float Gravity = -1.0f;

        protected override void Start()
        {
            base.Start();
            foreach (GameObject character in Characters)
            {
                Rigidbody rigidbody = character.GetComponent<Rigidbody>();
                rigidbody.useGravity = false;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
            foreach (GameObject entity in Entities)
            {
                Rigidbody rigidbody = entity.GetComponent<Rigidbody>();
                rigidbody.useGravity = false;
                rigidbody.angularDrag = 1.0f;
            }
        }

        protected void FixedUpdate()
        {
            foreach (GameObject character in Characters)
            {
                ApplyPhysics(character);
                StandUp(character);
            }
            foreach (GameObject entity in Entities)
            {
                ApplyPhysics(entity);
            }
        }

        private void ApplyPhysics(GameObject entity)
        {
            Vector3 to = (entity.transform.position - transform.position).normalized;
            entity.GetComponent<Rigidbody>().AddForce(to * Gravity);
        }

        private void StandUp(GameObject entity)
        {
            Vector3 to = (entity.transform.position - transform.position).normalized;
            entity.transform.rotation = Quaternion.FromToRotation(entity.transform.up.normalized, to) * entity.transform.rotation;
        }
    }
}

