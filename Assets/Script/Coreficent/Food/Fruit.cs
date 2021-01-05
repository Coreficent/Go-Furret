namespace Coreficent.Food
{
    using Coreficent.Physics;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Fruit : Edible
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

        public void HideMesh(int index)
        {
            index = Mathf.Clamp(index, 0, 3);
            _skinnedMeshRenderers[index].enabled = false;
        }

        public void ShowMesh(int index)
        {
            index = Mathf.Clamp(index, 0, 3);
            _skinnedMeshRenderers[index].enabled = true;
        }

        public void Pool()
        {
            DisablePhysics();
            HideMesh(0);
            HideMesh(1);
            HideMesh(2);
            HideMesh(3);
            transform.rotation = Quaternion.identity;
            transform.position = Vector3.zero;
        }

        public void Poll()
        {
            EnablePhysics();
            ShowMesh(0);
            ShowMesh(1);
            ShowMesh(2);
            ShowMesh(3);
        }

        public void EnablePhysics()
        {
            _planet.Entities.Add(gameObject);
            _rigidbody.constraints = RigidbodyConstraints.None;
            _rigidbody.isKinematic = false;
            _collider.enabled = true;
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
