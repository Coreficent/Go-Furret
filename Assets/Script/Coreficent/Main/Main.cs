﻿namespace Coreficent.Main
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Coreficent.Utility;
    using Coreficent.Control;

    public class Main : MonoBehaviour
    {
        public GameObject Furret;

        private Controller _controller;
        private void Start()
        {
            SanityCheck.Check(this, Furret);
            _controller = new Controller(Furret.GetComponent<Animator>());
        }
        private void Update()
        {
            /*
                check for user input
                run AI
                move enemies
                resolve collisions
                draw graphics
                play sounds
             */
            _controller.Run();
        }
    }
}