using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ObjectPool
{
    internal sealed class ViewServices
    {
        private readonly Dictionary<int, ObjectPool> _viewCache 
            = new Dictionary<int, ObjectPool>(12);
        
        public void Create(GameObject prefab)
        {
            if (!_viewCache.TryGetValue(prefab.GetInstanceID(), out ObjectPool viewPool))
            {
                viewPool = new ObjectPool(prefab);
                _viewCache[prefab.GetInstanceID()] = viewPool;
                Debug.Log(prefab.GetInstanceID());
            }

            var go  = viewPool.GetFromPool();
            Debug.Log(go.GetInstanceID());
            
        }

        public void Destroy(GameObject prefab)
        {
            Debug.Log(prefab.GetInstanceID());
   
            _viewCache[prefab.GetInstanceID()].ReturnToPool(prefab); 
        }
    }
}
