namespace Coreficent.Food
{
    using Coreficent.Utility;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class Bean : Edible
    {
        public enum BeanPattern
        {
            Gray,
            Grass,
            Fire,
            Water,
            Electric,
            Ice,
            Fighting,
            Ghost,
            Rainbow
        }

        [SerializeField] private Texture _gray;
        [SerializeField] private Texture _grass;
        [SerializeField] private Texture _fire;
        [SerializeField] private Texture _water;
        [SerializeField] private Texture _electric;
        [SerializeField] private Texture _ice;
        [SerializeField] private Texture _fighting;
        [SerializeField] private Texture _ghost;
        [SerializeField] private Texture _rainbow;

        public float ConsumeTime = 1.0f;

        private readonly List<MeshRenderer> _meshRenderers = new List<MeshRenderer>();

        private Color _color = Color.clear;
        private bool _pooled = false;
        private BeanPattern _pattern = BeanPattern.Gray;

        public Color Color
        {
            get => _color;
            set
            {
                SetShaderProperty(material => material.SetColor("_Color", value));
                _color = value;
            }
        }

        public BeanPattern Pattern
        {
            get => _pattern;
            set
            {
                SetShaderProperty(material =>
                {
                    switch (value)
                    {
                        case BeanPattern.Gray:
                            material.SetTexture("_MainTex", _gray);
                            _pattern = BeanPattern.Gray;
                            ConsumeTime = 2.0f;
                            break;

                        case BeanPattern.Grass:
                            material.SetTexture("_MainTex", _grass);
                            _pattern = BeanPattern.Grass;
                            ConsumeTime = 4.0f;
                            break;

                        case BeanPattern.Fire:
                            material.SetTexture("_MainTex", _fire);
                            _pattern = BeanPattern.Fire;
                            ConsumeTime = 4.0f;
                            break;

                        case BeanPattern.Water:
                            material.SetTexture("_MainTex", _water);
                            _pattern = BeanPattern.Water;
                            ConsumeTime = 4.0f;
                            break;

                        case BeanPattern.Electric:
                            material.SetTexture("_MainTex", _electric);
                            _pattern = BeanPattern.Electric;
                            ConsumeTime = 6.0f;
                            break;

                        case BeanPattern.Ice:
                            material.SetTexture("_MainTex", _ice);
                            _pattern = BeanPattern.Ice;
                            ConsumeTime = 6.0f;
                            break;

                        case BeanPattern.Fighting:
                            material.SetTexture("_MainTex", _fighting);
                            _pattern = BeanPattern.Fighting;
                            ConsumeTime = 8.0f;
                            break;

                        case BeanPattern.Ghost:
                            material.SetTexture("_MainTex", _ghost);
                            _pattern = BeanPattern.Ghost;
                            ConsumeTime = 8.0f;
                            break;

                        case BeanPattern.Rainbow:
                            material.SetTexture("_MainTex", _rainbow);
                            _pattern = BeanPattern.Rainbow;
                            ConsumeTime = 10.0f;
                            break;

                        default:
                            DebugLogger.Log("unexpected pattern type");
                            break;
                    }
                });
            }
        }

        public override bool Pooled
        {
            get => _pooled;
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
            SanityCheck.Check(this, _gray, _fire, _water, _electric, _ice, _fighting, _ghost, _rainbow);

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

        private void SetShaderProperty(Action<Material> action)
        {
            foreach (MeshRenderer meshRenderer in _meshRenderers)
            {
                foreach (Material material in meshRenderer.sharedMaterials)
                {
                    action(material);
                }
            }
        }
    }
}
