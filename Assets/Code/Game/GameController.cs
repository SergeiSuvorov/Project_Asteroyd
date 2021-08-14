using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletGameObject;
        [SerializeField] private GameObject _shipGameObject;
        [SerializeField] private GameObject _playerGameObject;
        [SerializeField] private ShipData _shipData;
        [SerializeField] private Transform _barrel;
        [SerializeField] private WeaponData _weaponData;

        private PlayerController _playerController;
        private ListExecuteObject _listExecuteObject;
        private Input _inputController;
        private AsterroidManager _asterroidManager;
        public ListExecuteObject ListExecuteObject { get { return _listExecuteObject; } }
        public Transform PlayerTransform => _shipGameObject.transform;
        void Awake()
        {
            GameStarter gameStarter = new GameStarter();
            gameStarter.CreateGame(_bulletGameObject, _shipGameObject, _shipData, _barrel, _weaponData, this, out _playerController, out _listExecuteObject, out _inputController, out _asterroidManager);
        }

        

        /// <summary>
        /// Метод проверяющий результаты ввода и передающий их в PlayerController
        /// </summary>
        /// <param name="verticalMove"></param>
        /// <param name="horizontalMove"></param>
        /// <param name="IsChangeAcceleretion"></param>
        /// <param name="IsShooting"></param>
        /// <param name="mousePosition"></param>
        public void CheckInputResult(float verticalMove, float horizontalMove, bool IsChangeAcceleretion, bool IsShooting, Vector3 mousePosition)
        {
            if (horizontalMove != 0 || verticalMove != 0)
                _playerController.Move(horizontalMove, verticalMove, IsChangeAcceleretion);

            if (IsShooting)
            {
                _playerController.Shooting();
            }
            if (IsChangeAcceleretion)
            {
                _playerController.ChangeAcceleretionMode();
            }

            _playerController.Rotation(mousePosition);
        }
    }
}
