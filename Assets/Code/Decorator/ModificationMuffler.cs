using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Decorator
{
    internal sealed class ModificationMuffler : ModificationWeapon
    {
        private readonly IMuffler _muffler;
        private readonly Transform _mufflerPosition;

        public ModificationMuffler( IMuffler muffler, Transform mufflerPosition)
        {
            _muffler = muffler;
            _mufflerPosition = mufflerPosition;
        }

        public override Weapon DeleteModification(Weapon weapon)
        {
            Object.Destroy(_muffler.MufflerInstance);
            weapon.SetBarrelPosition(_mufflerPosition);
            return weapon;
        }

        protected override Weapon AddModification(Weapon weapon, out GameObject muffler)
        {
            muffler = Object.Instantiate(_muffler.MufflerInstance, _mufflerPosition.position, Quaternion.identity);
            weapon.SetBarrelPosition(_muffler.BarrelPositionMuffler);

            return weapon;
        }
    }
}
