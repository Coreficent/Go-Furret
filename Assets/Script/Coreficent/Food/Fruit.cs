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

            _planet.Entities.Add(gameObject);

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

        protected void Update()
        {

        }
    }
}
