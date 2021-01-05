namespace Coreficent.Food
{
    using UnityEngine;

    public abstract class Edible : MonoBehaviour
    {
        public abstract void Feed(float percentage);
    }
}
