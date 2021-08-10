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
        private InputController _inputController;
        private AsterroidManager _asterroidManager;
        public ListExecuteObject ListExecuteObject { get { return _listExecuteObject; } }
        public Transform PlayerTransform => _shipGameObject.transform;
        void Awake()
        {
            _listExecuteObject = new ListExecuteObject();

            _playerController = new PlayerController( _bulletGameObject, _shipGameObject, _shipData, _barrel, _weaponData);

            _inputController = new InputController(this);
            _listExecuteObject.AddExecuteObject(_inputController);

            _asterroidManager = new AsterroidManager();
            _listExecuteObject.AddExecuteObject(_asterroidManager);

            Execute execute = FindObjectOfType<Execute>();
            execute.SetListtExecuteObject(_listExecuteObject);
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
