using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Builder
{
    internal sealed class AmmoGameObjectBuilder : GameObjectBuilder
    {
        public AmmoGameObjectBuilder(GameObject gameObject) : base(gameObject) { }
        public AmmoGameObjectBuilder() : base() { }

        public AmmoGameObjectBuilder AmmoLogic => new AmmoGameObjectBuilder(_gameObject);

        public static implicit operator GameObject(AmmoGameObjectBuilder builder)
        {
            return builder._gameObject;
        }

        public AmmoGameObjectBuilder Bullet()
        {
            var component = GetOrAddComponent<Bullet>();
            return this;
        }
    }
}
