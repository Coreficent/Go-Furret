namespace Coreficent.Physics
{
    using Coreficent.Utility;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class KinematicObject : MonoBehaviour
    {
        protected Rigidbody _rigidbody;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            SanityCheck.Check(this, _rigidbody);

            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}