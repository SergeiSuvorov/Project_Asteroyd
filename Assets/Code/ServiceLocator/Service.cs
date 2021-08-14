using UnityEngine;

namespace Asteroids.ServiceLocator
{
    internal sealed class Service : IService
    {
        public void Test()
        {
            Debug.Log(nameof(Service));
        }
    }
}
