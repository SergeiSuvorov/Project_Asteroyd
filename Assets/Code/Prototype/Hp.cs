using System;
using UnityEngine;

namespace Asteroids.Prototype
{
    [Serializable] 
    public class Hp 
    {
        public float MaxHP;
        public float CurrentHP;
        
        public override string ToString()
        {
            return $"MaxHP {MaxHP} CurrentHP {CurrentHP}";
        }
    }
}
