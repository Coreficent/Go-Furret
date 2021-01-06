namespace Coreficent.Food
{
    using Coreficent.Setting;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Recipe : MonoBehaviour
    {
        [SerializeField] private Bean _bean;

        public Bean CurrentBean
        {
            get => _bean;
        }

        protected void Start()
        {
            SanityCheck.Check(this, _bean);
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
                    SummonBean();
                    _bean.Color = fruits[0].Color;
                    _bean.Pattern = Bean.BeanPattern.Gray;
                    break;

                case 2:
                    SummonBean();
                    _bean.Color = Color.white;

                    _bean.Pattern = Bean.BeanPattern.Gray;
                    if (fruits[0].Species == Fruit.FruitSpecies.Razz && fruits[1].Species == Fruit.FruitSpecies.Pinap)
                    {
                        _bean.Pattern = Bean.BeanPattern.Grass;
                    }
                    else if (fruits[0].Species == Fruit.FruitSpecies.Razz && fruits[1].Species == Fruit.FruitSpecies.Nanab)
                    {
                        _bean.Pattern = Bean.BeanPattern.Fire;
                    }
                    else if (fruits[0].Species == Fruit.FruitSpecies.Pinap && fruits[1].Species == Fruit.FruitSpecies.Razz)
                    {
                        _bean.Pattern = Bean.BeanPattern.Water;
                    }
                    else if (fruits[0].Species == Fruit.FruitSpecies.Pinap && fruits[1].Species == Fruit.FruitSpecies.Nanab)
                    {
                        _bean.Pattern = Bean.BeanPattern.Electric;
                    }
                    else if (fruits[0].Species == Fruit.FruitSpecies.Nanab && fruits[1].Species == Fruit.FruitSpecies.Razz)
                    {
                        _bean.Pattern = Bean.BeanPattern.Ice;
                    }
                    else if (fruits[0].Species == Fruit.FruitSpecies.Nanab && fruits[1].Species == Fruit.FruitSpecies.Pinap)
                    {
                        if (Configuration.Singleton.Version == Configuration.ApplicationVersion.Nova)
                        {
                            _bean.Pattern = Bean.BeanPattern.Fighting;
                        }
                        else if (Configuration.Singleton.Version == Configuration.ApplicationVersion.Umbra)
                        {
                            _bean.Pattern = Bean.BeanPattern.Ghost;
                        }
                        else
                        {
                            DebugLogger.Warn("unexpected application version from configuration");
                        }
                    }
                    else
                    {
                        DebugLogger.Warn("unexpected pattern combination for two item recipe");
                    }
                    break;

                case 3:
                    break;

                default:
                    break;
            }
        }

        private void SummonBean()
        {
            _bean.Pooled = false;
            _bean.transform.localScale *= 0.01f;
            _bean.transform.position = transform.position + transform.TransformDirection(new Vector3(0.0f, 1.0f, 0.0f));
        }
    }
}
