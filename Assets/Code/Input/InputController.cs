using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class InputController : IExecute
    {
        private float _horizontalMoveInput;
        private float _verticalMoveInput;
        private GameController _gameController;
        public bool IsActive { get; private set; }

        public InputController(GameController GameController)
        {
            _gameController = GameController;
            IsActive = true;
        }

        /// <summary>
        /// Метод проверяющий нажатия клавиатуры - мыши и отправляющий результаты в GameController
        /// </summary>
        public void CheckInput()
        {

            _horizontalMoveInput = 0;
            _verticalMoveInput = 0;
            bool isShooting = false;
            bool IsChangeAcceleretion = false;

            _horizontalMoveInput = Input.GetAxis("Horizontal");
            _verticalMoveInput = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Fire1"))
            {
                isShooting = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
            {
                IsChangeAcceleretion = true;
            }

            var mousePosition = Input.mousePosition;

            _gameController.CheckInputResult(_verticalMoveInput, _horizontalMoveInput, IsChangeAcceleretion, isShooting, mousePosition);

        }

        public void Execute()
        {
            CheckInput();
        }
    }
}
