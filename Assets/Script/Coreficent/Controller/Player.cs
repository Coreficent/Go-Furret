﻿namespace Coreficent.Controller
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Player : MonoBehaviour
    {
        private float _turnAmount = 50.0f;
        void Start()
        {

        }

        void Update()
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime * _turnAmount);
        }
    }
}
