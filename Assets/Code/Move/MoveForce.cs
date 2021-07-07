using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    internal class MoveForce : MoveTransform
    {

        private Vector3 _move;
        private Rigidbody2D rigidbodyShip;


        public MoveForce(Transform transform, float engineForce) : base(transform, engineForce)
        {
            rigidbodyShip = transform.gameObject.GetComponent<Rigidbody2D>();
        }
        public override void Move(float horizontal, float vertical, float deltaTime)
        {

            var direction = new Vector2(horizontal, vertical);
            rigidbodyShip.AddForce(direction * EngineForce/(deltaTime), ForceMode2D.Impulse);
        }

    }
}
