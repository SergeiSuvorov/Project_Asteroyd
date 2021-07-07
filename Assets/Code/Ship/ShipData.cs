using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShip", menuName = "Data/Items/Ship")]
public class ShipData : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _healthPoint;

    public float Speed => _speed;
    public float Acceleration => _acceleration;
    public float HealthPoint => _healthPoint;

}
