using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Input : IExecute
    {
        private float _horizontalMoveInput;
        private float _verticalMoveInput;
        private GameController _gameController;
        public bool IsActive { get; private set; }

        public Input(GameController GameController)
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

            _horizontalMoveInput = UnityEngine.Input.GetAxis("Horizontal");
            _verticalMoveInput = UnityEngine.Input.GetAxis("Vertical");

            if (UnityEngine.Input.GetButtonDown("Fire1"))
            {
                isShooting = true;
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift) || UnityEngine.Input.GetKeyUp(KeyCode.LeftShift))
            {
                IsChangeAcceleretion = true;
            }

            var mousePosition = UnityEngine.Input.mousePosition;

            _gameController.CheckInputResult(_verticalMoveInput, _horizontalMoveInput, IsChangeAcceleretion, isShooting, mousePosition);

        }

        public void Execute()
        {
            CheckInput();
        }
    }
}
