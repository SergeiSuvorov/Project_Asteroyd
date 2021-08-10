using System;
using UnityEngine;

namespace Asteroids.Prototype
{
    [Serializable]
    public class PlayerData :  ICloneable
    {
        public float Speed;
        public Hp Hp;
        public GameObject gameObject;
        public override string ToString()
        {
            return $"Speed {Speed} Hp {Hp}";
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
