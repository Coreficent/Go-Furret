namespace Coreficent.Food
{
    using Coreficent.Physics;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Fruit : MonoBehaviour
    {
        [SerializeField] private Planet _planet;

        private readonly List<SkinnedMeshRenderer> _skinnedMeshRenderers = new List<SkinnedMeshRenderer>();

        private Collider _collider;
        private Rigidbody _rigidbody;

        protected void Start()
        {
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();

            SanityCheck.Check(this, _planet, _collider, _rigidbody);

            foreach (Transform i in transform.Find("Display").transform)
            {
                SkinnedMeshRenderer skinnedMeshRenderer = i.GetComponent<SkinnedMeshRenderer>();
                if (skinnedMeshRenderer != null)
                {
                    _skinnedMeshRenderers.Add(skinnedMeshRenderer);
                }
            }

            EnablePhysics();

            DebugLogger.Start(this);
        }

        protected void Update()
        {

        }

        public void DisplayState(int state)
        {
            DisablePhysics();
            for (int i = 0; i < Mathf.Min(state, 4); ++i)
            {
                _skinnedMeshRenderers[i].enabled = false;
            }
            if (state == 4)
            {
                Pool();
            }
        }

        public void Pool()
        {
            DisablePhysics();
            transform.rotation = Quaternion.identity;
            transform.position = Vector3.zero;
        }

        public void EnablePhysics()
        {
            _planet.Entities.Add(gameObject);
        }

        public void DisablePhysics()
        {
            _planet.Entities.Remove(gameObject);
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
        }
    }
}
