using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class ShipGameComandController : ShipController
    {
        public ShipGameComandController(GameObject shipGameObject, ShipData shipData): base(shipGameObject, shipData)
        {

        }

        public void OnRestartGame()
        {
            ShipGameObjectSetActive(true);
            ShipGameObjectSetPosition(Vector2.zero);
        }
        
    }
}
