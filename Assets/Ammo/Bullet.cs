using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D _bulletRigidbody;
    private float shootStartForce;
  
    public void Shooting(Vector3 direction, float shootStartForce)
    {
       
        _bulletRigidbody.AddForce(direction * shootStartForce);
        Destroy(gameObject, 3);
    }

   
}
