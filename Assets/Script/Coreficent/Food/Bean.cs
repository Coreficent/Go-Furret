namespace Coreficent.Food
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Bean : Edible
    {
        private readonly List<MeshRenderer> _meshRenderers = new List<MeshRenderer>();

        // Start is called before the first frame update
        protected void Start()
        {
            foreach (Transform i in transform)
            {
                MeshRenderer meshRenderer = i.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    _meshRenderers.Add(meshRenderer);
                }
            }

            Pool();

            DebugLogger.Start(this);
        }

        public override void Feed(float percentage)
        {
            percentage = Mathf.Clamp(percentage, 0.0f, 1.0f);
            int index = (int)(percentage * _meshRenderers.Count);
            ShowMesh(index);
        }

        public override void Pool()
        {
            HideAllMesh();
            transform.rotation = Quaternion.identity;
            transform.position = Vector3.zero;
        }

        public override void Poll()
        {
            HideAllMesh();
            ShowMesh(0);
        }

        public void ShowMesh(int index)
        {
            HideAllMesh();
            index = Mathf.Clamp(index, 0, _meshRenderers.Count - 1);
            _meshRenderers[index].enabled = true;
        }

        public void HideMesh(int index)
        {
            ShowAllMesh();
            index = Mathf.Clamp(index, 0, _meshRenderers.Count - 1);
            _meshRenderers[index].enabled = false;
        }

        private void ShowAllMesh()
        {
            foreach (MeshRenderer meshRenderer in _meshRenderers)
            {
                meshRenderer.enabled = true;
            }
        }

        private void HideAllMesh()
        {
            foreach (MeshRenderer meshRenderer in _meshRenderers)
            {
                meshRenderer.enabled = false;
            }
        }
    }
}
