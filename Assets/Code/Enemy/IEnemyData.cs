using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Asteroids
{
    public interface IEnemyData
    {
        GameObject EnemyGameObject { get; }
        Health Health { get; }
        IEnemyData GetEnemyData();
    }
}
   
