using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Decorator
{
    internal abstract class ModificationWeapon : IFire
    {
        private Weapon _weapon;

        protected abstract Weapon AddModification(Weapon weapon, out GameObject weaponModification);
        public abstract Weapon DeleteModification(Weapon weapon);

        public void ApplyModification(Weapon weapon, out GameObject weaponModification)
        {
            _weapon = AddModification(weapon, out weaponModification);
        }

        public void Fire()
        {
            _weapon.Fire();
        }
    }
}


