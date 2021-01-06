namespace Coreficent.Food
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Recipe : MonoBehaviour
    {
        [SerializeField] private Bean _regularBean;

        private Bean _bean;

        public Bean CurrentBean
        {
            get => _bean;
        }

        protected void Start()
        {
            _bean = _regularBean;

            SanityCheck.Check(this, _regularBean, _bean);
        }

        public bool CanProduce(List<Fruit> fruits)
        {
            return fruits.Count > 0 && fruits.Count < 4;
        }

        public void Produce(List<Fruit> fruits)
        {
            switch (fruits.Count)
            {
                case 0:
                    DebugLogger.Warn("tried to produce without a fruit");
                    break;
                case 1:
                    _bean = _regularBean;
                    _bean.Pooled = false;
                    _bean.transform.localScale *= 0.1f;
                    _bean.transform.position = transform.position + transform.TransformDirection(new Vector3(0.0f, 1.0f, 0.0f));
                    _bean.Color = fruits[0].Color;
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }
    }
}
