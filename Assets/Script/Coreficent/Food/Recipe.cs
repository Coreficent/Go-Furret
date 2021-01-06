namespace Coreficent.Food
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Recipe : MonoBehaviour
    {
        [SerializeField] private Bean _regularBean;

        public Bean CurrentBean
        {
            get => _bean;
        }

        private Bean _bean;


        protected void Start()
        {
            _bean = _regularBean;

            SanityCheck.Check(this, _regularBean, _bean);
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
