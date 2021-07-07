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

        private PlayerController playerController;
        private ListExecuteObject listExecuteObject;
        private InputController inputController;

        public Transform PlayerTransform => _shipGameObject.transform;
        private void Awake()
        {
            playerController = new PlayerController( _bulletGameObject, _shipGameObject, _shipData, _barrel, _weaponData);
            inputController = new InputController(this);

            listExecuteObject = new ListExecuteObject();
            listExecuteObject.AddExecuteObject(inputController);

            Execute execute = FindObjectOfType<Execute>();
            execute.SetListtExecuteObject(listExecuteObject);

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
                playerController.Move(horizontalMove, verticalMove, IsChangeAcceleretion);

            if (IsShooting)
            {
                playerController.Shooting();
            }
            if (IsChangeAcceleretion)
            {
                playerController.ChangeAcceleretionMode();
            }

            playerController.Rotation(mousePosition);
        }
    }
}
