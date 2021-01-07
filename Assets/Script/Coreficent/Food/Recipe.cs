namespace Coreficent.Food
{
    using Coreficent.Setting;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Recipe : MonoBehaviour
    {
        [SerializeField] private Bean _bean;

        public Bean Bean
        {
            get => _bean;
        }

        protected void Start()
        {
            SanityCheck.Check(this, _bean);
        }

        public float CalculateCookTime(List<Fruit> fruits)
        {
            return fruits.Count * 2.0f;
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
                    SummonBean(fruits[0].Color, Bean.BeanPattern.Gray);
                    break;

                case 2:
                    Bean.BeanPattern pattern = Bean.BeanPattern.Gray;
                    if (fruits[0].Species == Fruit.FruitSpecies.Razz && fruits[1].Species == Fruit.FruitSpecies.Pinap)
                    {
                        pattern = Bean.BeanPattern.Grass;
                    }
                    else if (fruits[0].Species == Fruit.FruitSpecies.Razz && fruits[1].Species == Fruit.FruitSpecies.Nanab)
                    {
                        pattern = Bean.BeanPattern.Fire;
                    }
                    else if (fruits[0].Species == Fruit.FruitSpecies.Pinap && fruits[1].Species == Fruit.FruitSpecies.Razz)
                    {
                        pattern = Bean.BeanPattern.Water;
                    }
                    else if (fruits[0].Species == Fruit.FruitSpecies.Pinap && fruits[1].Species == Fruit.FruitSpecies.Nanab)
                    {
                        pattern = Bean.BeanPattern.Electric;
                    }
                    else if (fruits[0].Species == Fruit.FruitSpecies.Nanab && fruits[1].Species == Fruit.FruitSpecies.Razz)
                    {
                        pattern = Bean.BeanPattern.Ice;
                    }
                    else if (fruits[0].Species == Fruit.FruitSpecies.Nanab && fruits[1].Species == Fruit.FruitSpecies.Pinap)
                    {
                        if (Configuration.Singleton.Version == Configuration.ApplicationVersion.Nova)
                        {
                            pattern = Bean.BeanPattern.Fighting;
                        }
                        else if (Configuration.Singleton.Version == Configuration.ApplicationVersion.Umbra)
                        {
                            pattern = Bean.BeanPattern.Ghost;
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

                    SummonBean(Color.white, pattern);
                    break;

                case 3:
                    bool rainbowProduct = true;

                    foreach (Fruit fruit in fruits)
                    {
                        if (fruit.Color == Fruit.Orange || fruit.Color == Fruit.Purple)
                        {
                            rainbowProduct = false;
                            break;
                        }
                    }

                    SummonBean(rainbowProduct ? Color.white : Color.black, rainbowProduct ? Bean.BeanPattern.Rainbow : Bean.BeanPattern.Gray);
                    break;

                default:
                    DebugLogger.Log("unexpected number of fruits when producing in recipe");
                    break;
            }
        }

        private void SummonBean(Color color, Bean.BeanPattern pattern)
        {
            _bean.Pooled = false;
            _bean.transform.localScale *= 0.01f;
            _bean.transform.position = transform.position + transform.TransformDirection(new Vector3(0.0f, 1.0f, 0.0f));
            _bean.Color = color;
            _bean.Pattern = pattern;
        }
    }
}
