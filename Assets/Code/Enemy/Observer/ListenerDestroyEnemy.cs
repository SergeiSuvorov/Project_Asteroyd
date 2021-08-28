using Asteroids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerDestroyEnemy 
{
    public void Add(IDestroy value)
    {
        value.IsDestroy += ValueOnIsDestroy;
    }

    public void Remove(IDestroy value)
    {
        value.IsDestroy -= ValueOnIsDestroy;
    }

    private void ValueOnIsDestroy(EnemyController enemy)
    {
        Debug.Log(enemy.EnemyGameObject.name + " был уничтожен");
    }

}
