namespace Coreficent.Food
{
    using Coreficent.Controller;
    using Coreficent.Physics;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Cooker : KinematicObject
    {
        private Recipe _recipe;

        private readonly List<Fruit> _fruitVacuum = new List<Fruit>();
        private readonly TimeController _timeController = new TimeController();

        private int _lastVacuumSize = 0;
        private bool _vacuumComplete = false;

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
            get { return _recipe.CookTime + Bean.CreateTime + 1.0f; }
        }

        public Bean Bean
        {
            get => _recipe.Bean;
        }

        protected override void Start()
        {
            base.Start();

            _recipe = GetComponent<Recipe>();

            SanityCheck.Check(this, _recipe);

            DebugLogger.Start(this);
        }

        protected void OnTriggerEnter(Collider other)
        {
            Throw(other);
        }

        protected void OnTriggerExit(Collider other)
        {
            Throw(other);
        }

        protected void Update()
        {
            switch (State)
            {
                case CookerState.Vacuum:
                    bool vacuumComplete = true;

                    foreach (Fruit fruit in _fruitVacuum)
                    {
                        float maximumHeightScaler = 1.0f;
                        float heightOffsetScaler = 0.5f;

                        float distance = new Vector2(transform.position.x - fruit.transform.position.x, transform.position.z - fruit.transform.position.z).sqrMagnitude;
                        Vector3 destination = transform.position + transform.up * distance * maximumHeightScaler + transform.up * heightOffsetScaler;

                        fruit.transform.position = Vector3.MoveTowards(fruit.transform.position, destination, 1.0f * Time.deltaTime);

                        if ((fruit.transform.position - destination).magnitude > 0.05f)
                        {
                            vacuumComplete = false;
                        }
                    }

                    _vacuumComplete = vacuumComplete;
                    break;

                case CookerState.Cook:
                    DebugLogger.Bug("cooking in cooker");

                    foreach (Fruit fruit in _fruitVacuum)
                    {
                        fruit.transform.localScale = Vector3.one * (1.0f - _timeController.Progress(_recipe.CookTime));
                    }

                    DebugLogger.Bug("_recipe.CalculateCookTime(_fruitVacuum)" + _recipe.CookTime);
                    if (_timeController.Passed(_recipe.CookTime))
                    {


                        foreach (Fruit fruit in _fruitVacuum)
                        {
                            fruit.Pooled = true;
                        }

                        _fruitVacuum.Clear();

                        GoTo(CookerState.Create);
                    }

                    break;

                case CookerState.Create:

                    _recipe.Bean.transform.localScale = Vector3.one * _timeController.Progress(Bean.CreateTime);

                    DebugLogger.Bug("Bean.CreateTime" + Bean.CreateTime);

                    if (_timeController.Passed(Bean.CreateTime))
                    {
                        GoTo(CookerState.Serve);
                    }

                    break;

                case CookerState.Serve:

                    DebugLogger.Log("serving");

                    if (_recipe.Bean.Pooled)
                    {
                        GoTo(CookerState.Vacuum);
                    }

                    break;

                default:
                    DebugLogger.Warn("unexpected cooker state");
                    return;
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
            return _vacuumComplete && _recipe.CanProduce(_fruitVacuum);
        }

        public void Cook()
        {
            _recipe.Produce(_fruitVacuum);
            GoTo(CookerState.Cook);
        }

        private void GoTo(CookerState nextState)
        {
            _timeController.Reset();
            State = nextState;
        }

        private void Throw(Collider other)
        {
            if (State == CookerState.Vacuum)
            {
                Fruit fruit = other.gameObject.GetComponent<Fruit>();
                if (fruit)
                {
                    fruit.DisablePhysics();
                    _fruitVacuum.Add(fruit);
                }
            }
        }
    }
}
