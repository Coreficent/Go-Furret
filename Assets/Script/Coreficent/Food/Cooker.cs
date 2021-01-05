﻿namespace Coreficent.Food
{
    using Coreficent.Physics;
    using Coreficent.Utility;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Cooker : KinematicObject
    {
        [SerializeField] private Bean _bean;

        private readonly List<GameObject> _fruitVacuum = new List<GameObject>();
        private int _lastVacuumSize = 0;

        private enum CookerState
        {
            Vacuum,
            Cook,
            Create
        }

        private CookerState _cookerState = CookerState.Vacuum;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            SanityCheck.Check(this, _bean);

            DebugLogger.Start(this);
        }

        // Update is called once per frame
        protected void Update()
        {
            switch (_cookerState)
            {
                case CookerState.Vacuum:
                    foreach (GameObject fruit in _fruitVacuum)
                    {
                        float maximumHeightScaler = 1.0f;
                        float heightOffsetScaler = 0.5f;

                        float distance = new Vector2(transform.position.x - fruit.transform.position.x, transform.position.z - fruit.transform.position.z).sqrMagnitude;
                        Vector3 destination = transform.position + transform.up * distance * maximumHeightScaler + transform.up * heightOffsetScaler;

                        fruit.transform.position = Vector3.MoveTowards(fruit.transform.position, destination, 1.0f * Time.deltaTime);
                    }
                    break;

                case CookerState.Cook:
                    foreach (GameObject fruit in _fruitVacuum)
                    {
                        fruit.transform.localScale *= 0.9f;

                        if (fruit.transform.localScale.magnitude < 0.1f)
                        {
                            _cookerState = CookerState.Create;
                        }
                    }

                    break;

                case CookerState.Create:

                    if (_bean.transform.localScale.x < 1.0f)
                    {
                        _bean.transform.localScale *= 1.1f;
                    }
                    else
                    {
                        _bean.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        _cookerState = CookerState.Vacuum;
                    }

                    break;

                default:
                    DebugLogger.Warn("unexpected cooker state");
                    return;
            }
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Fruit>())
            {
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.gameObject.GetComponent<Collider>().enabled = false;

                _fruitVacuum.Add(other.gameObject);
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Fruit>())
            {
                _fruitVacuum.Remove(other.gameObject);
            }
        }

        public bool FruitVacuumSizeChanged()
        {
            if (_lastVacuumSize == _fruitVacuum.Count)
            {
                return false;
            }
            else
            {
                _lastVacuumSize = _fruitVacuum.Count;
                return true;
            }
        }

        public void Cook()
        {
            DebugLogger.ToDo("disable vaccuum");

            _bean.transform.localScale *= 0.1f;

            _cookerState = CookerState.Cook;
        }
    }
}
