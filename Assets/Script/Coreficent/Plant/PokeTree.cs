namespace Coreficent.Plant
{
    using Coreficent.Physics;
    using Coreficent.Utility;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PokeTree : KinematicObject
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            DebugLogger.Start(this);
        }

        // Update is called once per frame
        protected void Update()
        {

        }
    }
}
