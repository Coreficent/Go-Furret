namespace Coreficent.Plant
{
    using Coreficent.Food;
    using Coreficent.Physics;
    using Coreficent.Utility;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PokeTree : KinematicObject
    {
        // Start is called before the first frame update

        [SerializeField] private Fruit _fruit;

        protected override void Start()
        {
            base.Start();

            SanityCheck.Check(this, _fruit);

            DebugLogger.Start(this);


        }

        // Update is called once per frame
        protected void Update()
        {

        }

        public void SpawnFruitNear(GameObject player)
        {
            _fruit.transform.position = player.transform.position + player.transform.up * 2.0f;
            _fruit.Pooled = false;
        }
    }
}
