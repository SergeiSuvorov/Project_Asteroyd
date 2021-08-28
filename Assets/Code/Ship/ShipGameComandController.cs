using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class ShipGameComandController : ShipController
    {
        public ShipGameComandController(ShipData shipData, WeaponData weaponData, Transform shipGameObjectParent) : base( shipData, weaponData, shipGameObjectParent)
        {

        }

        public void OnRestartGame()
        {
            ShipGameObjectSetActive(true);
            ShipGameObjectSetPosition(Vector2.zero);
        }
        
    }
}
