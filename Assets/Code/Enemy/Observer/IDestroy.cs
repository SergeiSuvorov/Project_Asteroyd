using Asteroids;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroy
{
    event Action<EnemyController> IsDestroy;
}
