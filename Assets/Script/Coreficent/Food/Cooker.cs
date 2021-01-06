namespace Coreficent.Food
{
    using Coreficent.Controller;
    using Coreficent.Physics;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Cooker : KinematicObject
    {
        [SerializeField] private Bean _bean;

        private readonly List<GameObject> _fruitVacuum = new List<GameObject>();
        private readonly TimeController _timeController = new TimeController();

        private int _lastVacuumSize = 0;
        private float _cookTime = 4.0f;
        private float _createTime = 2.0f;

        public enum CookerState
        {
            Vacuum,
            Cook,
            Create,
            Serve
        }

        public CookerState State = CookerState.Vacuum;

        public float RecipeTime
        {
            get { return _cookTime + _createTime; }
        }

        protected override void Start()
        {
            base.Start();

            SanityCheck.Check(this, _bean);

            DebugLogger.Log("pool test polled");

            DebugLogger.Start(this);
        }

        protected void Update()
        {
            switch (State)
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
                    DebugLogger.Bug("cooking in cooker");

                    foreach (GameObject fruit in _fruitVacuum)
                    {
                        fruit.transform.localScale = Vector3.one * (1.0f - _timeController.Progress(_cookTime));
                    }

                    if (_timeController.Passed(_cookTime))
                    {
                        _timeController.Reset();

                        foreach (GameObject fruit in _fruitVacuum)
                        {
                            fruit.GetComponent<Fruit>().Pooled = true;
                        }

                        _fruitVacuum.Clear();
                        State = CookerState.Create;
                    }

                    break;

                case CookerState.Create:

                    _bean.transform.localScale = Vector3.one * _timeController.Progress(_createTime);

                    if (_timeController.Passed(_createTime))
                    {
                        State = CookerState.Serve;
                    }

                    break;

                case CookerState.Serve:

                    DebugLogger.Log("serving");

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
                other.gameObject.GetComponent<Fruit>().DisablePhysics();

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
                DebugLogger.Log("_lastVacuumSize", _lastVacuumSize);
                DebugLogger.Log("_fruitVacuum.Count", _fruitVacuum.Count);

                _lastVacuumSize = _fruitVacuum.Count;
                return true;
            }
        }

        public bool CanCook()
        {
            return _fruitVacuum.Count > 0;
        }

        public void Cook()
        {
            DebugLogger.ToDo("disable vaccuum");

            _bean.Pooled = false;
            _bean.transform.localScale *= 0.1f;
            _bean.transform.position = transform.position + transform.TransformDirection(new Vector3(0.0f, 1.0f, 0.0f));

            _timeController.Reset();
            State = CookerState.Cook;
        }

        public void Feed(float percentage)
        {
            _bean.Feed(percentage);
        }
    }
}
