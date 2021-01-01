namespace Coreficent.Physics
{
    using UnityEngine;

    public class Planet : StationaryObject
    {
        public float Gravity = -1.0f;

        protected override void Awake()
        {
            base.Awake();
        }
        public void CalculatePhysics(GameObject entity)
        {
            Vector3 to = (entity.transform.position - transform.position).normalized;
            entity.transform.rotation = Quaternion.FromToRotation(entity.transform.up.normalized, to) * entity.transform.rotation;
            entity.GetComponent<Rigidbody>().AddForce(to * Gravity);
        }
    }
}

