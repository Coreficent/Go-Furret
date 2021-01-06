namespace Coreficent.Plant
{
    using Coreficent.Food;
    using Coreficent.Physics;
    using Coreficent.Utility;
    using UnityEngine;

    public class PokeTree : KinematicObject
    {

        [SerializeField] private Fruit _fruit;

        protected override void Start()
        {
            base.Start();

            SanityCheck.Check(this, _fruit);

            DebugLogger.Start(this);


        }

        public void SpawnFruitNear(GameObject player)
        {
            if (_fruit.Pooled)
            {
                _fruit.transform.position = player.transform.position + player.transform.up * 2.0f;
                _fruit.Pooled = false;
            }
        }
    }
}
