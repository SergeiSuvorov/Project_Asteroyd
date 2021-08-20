using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Command
{
    internal abstract class BaseUI : MonoBehaviour
    {
        public abstract void Execute();
        public abstract void Cancel();
    }
}

