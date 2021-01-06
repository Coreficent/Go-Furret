namespace Coreficent.Food
{
    using UnityEngine;

    public abstract class Edible : MonoBehaviour
    {
        public abstract bool Pooled { get; set; }
        public abstract void Feed(float percentage);
    }
}
