using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Visitor
{
    public interface IActivateAtScene
    {
        void ActivateAtScene(AsterroidController asterroidController);
    }
}

