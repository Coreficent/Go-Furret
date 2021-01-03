namespace Food.Fruit
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Fruit : MonoBehaviour
    {
        private readonly List<SkinnedMeshRenderer> _skinnedMeshRenderers = new List<SkinnedMeshRenderer>();

        protected void Start()
        {
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
