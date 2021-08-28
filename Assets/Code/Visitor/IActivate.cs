using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Visitor
{
    public interface IActivate
    {
        void Activate(IActivateAtScene value);
    }
}

