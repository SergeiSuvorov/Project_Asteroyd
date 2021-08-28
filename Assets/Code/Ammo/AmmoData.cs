using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAmmo", menuName = "Data/Items/Ammo")]
public class AmmoData : ScriptableObject
{
    [SerializeField] private float _damage;
    public float Damage => _damage;
    [SerializeField] private GameObject _ammoGO;
    public GameObject AmmoGO => _ammoGO;
}
