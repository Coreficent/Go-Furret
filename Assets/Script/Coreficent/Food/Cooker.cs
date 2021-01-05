﻿namespace Coreficent.Food
{
    using Coreficent.Utility;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Cooker : MonoBehaviour
    {
        private List<GameObject> _fruitVacuum = new List<GameObject>();

        // Start is called before the first frame update
        protected void Start()
        {

        }

        // Update is called once per frame
        protected void Update()
        {
            foreach (GameObject fruit in _fruitVacuum)
            {
                float maximumHeightScaler = 1.0f;
                float heightOffsetScaler = 0.5f;

                float distance = Vector2.SqrMagnitude(new Vector2(transform.position.x - fruit.transform.position.x, transform.position.z - fruit.transform.position.z));
                Vector3 destination = transform.position + transform.up * distance * maximumHeightScaler + transform.up * heightOffsetScaler;

                fruit.transform.position = Vector3.MoveTowards(fruit.transform.position, destination, 1.0f * Time.deltaTime);
            }
        }

        protected void OnTriggerEnter(Collider other)
        {
            DebugLogger.Log("trigger exit", other.gameObject.name);

            if (other.gameObject.GetComponent<Fruit>())
            {
                //other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.gameObject.GetComponent<SphereCollider>().enabled = false;

                //other.gameObject.GetComponent<Animator>().Play("Cook");

                _fruitVacuum.Add(other.gameObject);
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            DebugLogger.Log("trigger exit", other.gameObject.name);

            if (other.gameObject.GetComponent<Fruit>())
            {
                //other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                _fruitVacuum.Remove(other.gameObject);
            }
        }
    }
}
