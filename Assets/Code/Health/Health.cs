using System;
using UnityEngine;

namespace Asteroids
{
    [Serializable]
    public sealed class Health
    {

        public float Max { get; }
        public float Current { get; private set; }
        
        public Health(float max, float current)
        {
            Max = max;
            Current = current;
        }

        public void ChangeCurrentHealth(float hp)
        {
            Current = hp;
        }

        public void Damage(float hp)
        {
            Current -= hp;
           
        }
        public void SetHealthAid(float healthAid)
        {
            Current += healthAid;
        }

        public override string ToString()
        {
            return $"MaxHP {Max} CurrentHP {Current}";
        }
    }
}
