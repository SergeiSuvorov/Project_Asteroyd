using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class InputController : IExecute
    {
        private float horizontalMoveInput;
        private float verticalMoveInput;
        private GameController gameController;
        public bool IsActive { get; private set; }

        public InputController(GameController GameController)
        {
            gameController = GameController;
            IsActive = true;
        }

        /// <summary>
        /// Метод проверяющий нажатия клавиатуры - мыши и отправляющий результаты в GameController
        /// </summary>
        public void CheckInput()
        {

            horizontalMoveInput = 0;
            verticalMoveInput = 0;
            bool isShooting = false;
            bool IsChangeAcceleretion = false;

            horizontalMoveInput = Input.GetAxis("Horizontal");
            verticalMoveInput = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Fire1"))
            {
                isShooting = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
            {
                IsChangeAcceleretion = true;
            }

            var mousePosition = Input.mousePosition;

            gameController.CheckInputResult(verticalMoveInput, horizontalMoveInput, IsChangeAcceleretion, isShooting, mousePosition);

        }

        public void Execute()
        {
            CheckInput();
        }
    }
}
