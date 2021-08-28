using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Visitor
{
    public class ActivateEnemyAtScene : IActivateAtScene
    {
        public void ActivateAtScene(AsterroidController asterroidController)
        {
            Debug.Log(asterroidController.EnemyGameObject.name + " замечен невдалеке");
        }
    }
}
