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
            get { return _recipe.CookTime + Bean.CreateTime + 0.5f; }
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
                        fruit.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, _timeController.Progress(_recipe.CookTime));
                        fruit.transform.position += (transform.position - fruit.transform.position) * 0.25f * Time.deltaTime;
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
                    float sizeScaler = 0.75f;

                    if (_recipe.Bean.Pattern == Bean.BeanPattern.Gray)
                    {
                        if (_recipe.Bean.Color == Fruit.Black)
                        {
                            sizeScaler = 1.0f;
                        }
                        else
                        {
                            sizeScaler = 0.5f;
                        }
                    }
                    if (_recipe.Bean.Pattern == Bean.BeanPattern.Rainbow)
                    {
                        sizeScaler = 1.25f;
                    }

                    _recipe.Bean.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * sizeScaler, _timeController.Progress(Bean.CreateTime));
                    _recipe.Bean.transform.position = Vector3.Lerp(transform.position + transform.TransformVector(Vector3.up * 0.35f), transform.position + transform.TransformVector(Vector3.up * 0.55f), _timeController.Progress(Bean.CreateTime));

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

        public bool CanCook()
        {
            return _vacuumComplete && _recipe.CanProduce(_fruitVacuum);
        }

        public void Cook()
        {
            _recipe.Produce(_fruitVacuum);
            GoTo(CookerState.Cook);
        }


        public bool CanThrow(Vector3 playerPosition)
        {
            return FruitVacuumSizeIncreased() && (playerPosition.magnitude - Mathf.Abs(_fruitVacuum[_fruitVacuum.Count - 1].transform.position.magnitude)) < 0.5f;
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

        public bool FruitVacuumSizeIncreased()
        {
            if (_lastVacuumSize < _fruitVacuum.Count)
            {
                DebugLogger.Log("_lastVacuumSize", _lastVacuumSize);
                DebugLogger.Log("_fruitVacuum.Count", _fruitVacuum.Count);

                _lastVacuumSize = _fruitVacuum.Count;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GoTo(CookerState nextState)
        {
            _timeController.Reset();
            State = nextState;
        }
    }
}
