using UnityEngine;

namespace Asteroids.Builder
{
    internal class GameObjectBuilder
    {
        protected GameObject _gameObject;
        
        public GameObjectBuilder() => _gameObject = new GameObject();
        protected GameObjectBuilder(GameObject gameObject) => _gameObject = gameObject;

        public GameObjectVisualBuilder Visual => new GameObjectVisualBuilder(_gameObject);
        public GameObjectPhysicsBuilder Physics => new GameObjectPhysicsBuilder(_gameObject);
  
        public static implicit operator GameObject(GameObjectBuilder builder)
        {
            return builder._gameObject;
        }

        protected T GetOrAddComponent<T>() where T : Component
        {
            var result = _gameObject.GetComponent<T>();
            if (!result)
            {
                result = _gameObject.AddComponent<T>();
            }
            return result;
        }
    }
}
