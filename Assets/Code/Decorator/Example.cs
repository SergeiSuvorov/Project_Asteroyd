using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Decorator
{
    internal sealed class Example : MonoBehaviour
    {
        private IFire _fire;
        private Weapon _weapon;
        private GameObject _mountedScope;
        private GameObject _mountedMaffler;

        [Header("Start Gun")]
        [SerializeField] private Rigidbody _bullet;
        [SerializeField] private Transform _barrelPosition;
        [SerializeField] private float _weaponForce;

        [Header("Muffler Gun")]
        [SerializeField] private Transform _barrelPositionMuffler;
        [SerializeField] private GameObject _muffler;


        [Header("Scope")]
        [SerializeField] private Transform _gunPositionScope;
        [SerializeField] private GameObject _scope;


        private void Start()
        {
            IAmmunition ammunition = new Bullet(_bullet, 3.0f);
            _weapon = new Weapon(ammunition, _barrelPosition, _weaponForce);
            _fire = _weapon;
        }

        private void AddMuffer()
        {
            var muffler = new Muffler(_barrelPositionMuffler, _barrelPositionMuffler, _muffler);
            ModificationWeapon modificationWeapon = new ModificationMuffler(muffler, _barrelPosition);
            modificationWeapon.ApplyModification(_weapon,out _mountedMaffler);

            _fire = modificationWeapon;
        }

        private void AddScope()
        {
            var scope = new Scope(_gunPositionScope, _scope);
            ModificationWeapon modificationWeapon = new ModificationScope(scope, _gunPositionScope.position);
            modificationWeapon.ApplyModification(_weapon, out _mountedScope);
            _fire = modificationWeapon;
        }

        private void DeleteMuffler()
        {
            var muffler = new Muffler(_barrelPositionMuffler, _barrelPositionMuffler, _mountedMaffler);
            ModificationWeapon modificationWeapon = new ModificationMuffler(muffler, _barrelPosition);
            _weapon = modificationWeapon.DeleteModification(_weapon);
            _fire = _weapon;
        }

        private void DeleteScope()
        {
            var scope = new Scope(_gunPositionScope, _mountedScope);
            ModificationWeapon modificationWeapon = new ModificationScope(scope, _gunPositionScope.position);
            _weapon = modificationWeapon.DeleteModification(_weapon);
            _fire = _weapon;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _fire.Fire();
            }
            if(UnityEngine.Input.GetMouseButtonDown(1))
            {
                if (_mountedMaffler == null)
                    AddMuffer();
                else
                    DeleteMuffler();
            }
            if (UnityEngine.Input.GetMouseButtonDown(2))
            {
                if (_mountedScope == null)
                    AddScope();
                else
                    DeleteScope();
            }
            
        }
    }
}

