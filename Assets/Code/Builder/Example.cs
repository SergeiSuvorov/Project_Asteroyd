using UnityEngine;

namespace Asteroids.Builder
{
    internal sealed class Example : MonoBehaviour
    {
        [SerializeField] private Sprite _sprite;
        
        private void Start()
        {
            var gameObjectBuilder = new GameObjectBuilder();

            GameObject player = gameObjectBuilder.Visual.Name("Roman").Sprite(_sprite).
                Physics.Rigidbody2D(5).BoxCollider2D();
        }
        
    }
}
