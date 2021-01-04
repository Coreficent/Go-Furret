namespace Food.Fruit
{
    using Coreficent.Physics;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Fruit : MonoBehaviour
    {
        [SerializeField] private Planet _planet;

        private readonly List<SkinnedMeshRenderer> _skinnedMeshRenderers = new List<SkinnedMeshRenderer>();

        protected void Start()
        {
            SanityCheck.Check(this, _planet);

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

        public void EnablePhysics()
        {
            _planet.Entities.Add(gameObject);
        }

        public void DisablePhysics()
        {
            _planet.Entities.Remove(gameObject);
        }
    }
}
