using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memento
{
    public class InputMemento 
    {
        private MementoGameController _mementoGameController;
        public InputMemento(MementoGameController mementoGameController)
        {
            _mementoGameController = mementoGameController;
        }

        public void GetInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _mementoGameController.StartRewind();
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                _mementoGameController.StopRewind();
            }
        }
    }
}
