using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Asteroids.Decorator
{
    internal sealed class ModificationScope : ModificationWeapon
    {
        private readonly IScope _scope;
        private readonly Vector3 _scopePosition;
        public ModificationScope(IScope scope, Vector3 scopePosition)
        {
            _scope = scope;
            _scopePosition = scopePosition;
        }

        public override Weapon DeleteModification(Weapon weapon)
        {
            Object.Destroy(_scope.ScopeInstance);
            return weapon;
        }

        protected override Weapon AddModification(Weapon weapon, out GameObject scope)
        {
            scope = Object.Instantiate(_scope.ScopeInstance, _scopePosition, Quaternion.identity);
            return weapon;
        }
    }
}
