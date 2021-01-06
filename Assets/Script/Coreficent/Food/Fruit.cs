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

        private bool _pooled = false;

        public override bool Pooled
        {
            get { return _pooled; }
            set
            {
                if (value)
                {
                    DisablePhysics();
                    HideAllMesh();
                    transform.rotation = Quaternion.identity;
                    transform.localScale = Vector3.zero;
                    transform.position = Vector3.zero;
                    _pooled = true;
                }
                else
                {
                    EnablePhysics();
                    ShowAllMesh();
                    transform.rotation = Quaternion.identity;
                    transform.localScale = Vector3.one;
                    _pooled = false;
                }
            }
        }

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

            DebugLogger.Start(this);
        }

        public override void Feed(float percentage)
        {
            percentage = Mathf.Clamp(percentage, 0.0f, 1.0f);
            int index = (int)(percentage * _skinnedMeshRenderers.Count);
            HideMesh(index);

            if (percentage == 1.0f)
            {
                Pooled = true;
            }
        }

        public void ShowMesh(int index)
        {
            index = Mathf.Clamp(index, 0, _skinnedMeshRenderers.Count - 1);
            _skinnedMeshRenderers[index].enabled = true;
        }

        public void HideMesh(int index)
        {
            index = Mathf.Clamp(index, 0, _skinnedMeshRenderers.Count - 1);
            _skinnedMeshRenderers[index].enabled = false;
        }

        private void ShowAllMesh()
        {
            foreach (SkinnedMeshRenderer meshRenderer in _skinnedMeshRenderers)
            {
                meshRenderer.enabled = true;
            }
        }

        private void HideAllMesh()
        {
            foreach (SkinnedMeshRenderer meshRenderer in _skinnedMeshRenderers)
            {
                meshRenderer.enabled = false;
            }
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
