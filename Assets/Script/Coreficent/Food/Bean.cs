namespace Coreficent.Food
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Bean : Edible
    {
        private readonly List<MeshRenderer> _meshRenderers = new List<MeshRenderer>();

        private bool _pooled = false;

        public override bool Pooled
        {
            get { return _pooled; }
            set
            {
                if (value)
                {
                    HideAllMesh();
                    transform.rotation = Quaternion.identity;
                    transform.localScale = Vector3.zero;
                    transform.position = Vector3.zero;
                    _pooled = true;
                }
                else
                {
                    HideAllMesh();
                    ShowMesh(0);
                    transform.rotation = Quaternion.identity;
                    transform.localScale = Vector3.one;
                    _pooled = false;
                }
            }
        }

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

            Pooled = true;

            DebugLogger.Start(this);
        }

        public override void Feed(float percentage)
        {
            percentage = Mathf.Clamp(percentage, 0.0f, 1.0f);
            int index = (int)(percentage * _meshRenderers.Count);
            ShowMesh(index);

            if (percentage == 1.0f)
            {
                Pooled = true;
            }
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

        public void ShowAllMesh()
        {
            foreach (MeshRenderer meshRenderer in _meshRenderers)
            {
                meshRenderer.enabled = true;
            }
        }

        public void HideAllMesh()
        {
            foreach (MeshRenderer meshRenderer in _meshRenderers)
            {
                meshRenderer.enabled = false;
            }
        }
    }
}
