using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShip", menuName = "Data/Items/Ship")]
public class ShipData : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _healthPoint;
    [SerializeField] private GameObject  _shipGO;
    [SerializeField] private MovingType movingType;
    [SerializeField] private Vector3 _weaponLocalPosition;
    public float Speed => _speed;
    public float Acceleration => _acceleration;
    public float HealthPoint => _healthPoint;
    public MovingType MovingType => movingType;
    public GameObject ShipGO => _shipGO;

    public Vector3 WeaponLocalPosition => _weaponLocalPosition;
}
public enum MovingType
{
    MoveTransform = 0,
    MoveForce =1,
    AcselerationMove =2,
    StateTranformMove =3
}
