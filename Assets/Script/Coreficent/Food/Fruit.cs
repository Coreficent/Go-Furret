namespace Coreficent.Food
{
    using Coreficent.Physics;
    using Coreficent.Setting;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Fruit : Edible
    {
        public static Color Orange = new Color(1.0f, 0.9f, 0.6f, 1.0f);
        public static Color Purple = new Color(0.7f, 0.6f, 1.0f, 1.0f);
        public static Color Yellow = new Color(1.0f, 1.0f, 0.6f, 1.0f);
        public static Color Green = new Color(0.6f, 1.0f, 0.6f, 1.0f);
        public static Color Red = new Color(1.0f, 0.7f, 0.6f, 1.0f);
        public static Color Pink = new Color(1.0f, 0.6f, 1.0f, 1.0f);
        public static Color Black = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        public static Color White = new Color(0.9f, 0.9f, 0.9f, 1.0f);

        public enum FruitSpecies
        {
            None,
            Razz,
            Pinap,
            Nanab
        }

        [SerializeField] private Planet _planet;
        [SerializeField] private Texture _textureOrange;
        [SerializeField] private Texture _texturePurple;
        [SerializeField] private Texture _textureNova;
        [SerializeField] private Texture _textureUmbra;

        public Color Color = Color.clear;
        public FruitSpecies Species = FruitSpecies.None;

        private readonly List<SkinnedMeshRenderer> _skinnedMeshRenderers = new List<SkinnedMeshRenderer>();

        private Collider _collider;
        private Rigidbody _rigidbody;

        private bool _pooled = false;

        public float ConsumeTime
        {
            get => Species == FruitSpecies.Razz ? 1.5f : Species == FruitSpecies.Pinap ? 2.0f : Species == FruitSpecies.Nanab ? 2.5f : 1.0f;
        }

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

                    int index = Random.Range(0, 3);

                    foreach (SkinnedMeshRenderer skinnedMeshRenderer in _skinnedMeshRenderers)
                    {
                        foreach (Material material in skinnedMeshRenderer.sharedMaterials)
                        {
                            switch (index)
                            {
                                case 0:
                                    material.SetTexture("_MainTex", _textureOrange);
                                    Color = Orange;
                                    break;

                                case 1:
                                    material.SetTexture("_MainTex", _texturePurple);
                                    Color = Purple;
                                    break;
                                case 2:
                                    material.SetTexture("_MainTex", _textureNova);
                                    Color = Black;
                                    if (Species == FruitSpecies.Razz)
                                    {
                                        Color = Red;
                                    }
                                    if (Species == FruitSpecies.Pinap)
                                    {
                                        Color = Yellow;
                                    }
                                    if (Species == FruitSpecies.Nanab)
                                    {
                                        if (Configuration.Singleton.Version == Configuration.ApplicationVersion.Nova)
                                        {
                                            Color = Pink;
                                        }
                                        if (Configuration.Singleton.Version == Configuration.ApplicationVersion.Umbra)
                                        {
                                            Color = Green;
                                        }
                                    }

                                    DebugLogger.Assert("exclusive color set", Color != Black);
                                    break;

                                default:
                                    DebugLogger.Warn("unexpected texture index in fruit");
                                    break;
                            }
                        }
                    }

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

            SanityCheck.Check(this, _planet, _collider, _rigidbody, Species != FruitSpecies.None);

            foreach (Transform i in transform.Find("Display").transform)
            {
                SkinnedMeshRenderer skinnedMeshRenderer = i.GetComponent<SkinnedMeshRenderer>();
                if (skinnedMeshRenderer != null)
                {
                    _skinnedMeshRenderers.Add(skinnedMeshRenderer);
                }
            }

            Pooled = false;

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

        public void ShowAllMesh()
        {
            foreach (SkinnedMeshRenderer meshRenderer in _skinnedMeshRenderers)
            {
                meshRenderer.enabled = true;
            }
        }

        public void HideAllMesh()
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
