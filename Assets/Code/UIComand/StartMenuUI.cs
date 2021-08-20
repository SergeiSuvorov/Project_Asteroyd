
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Command
{
    internal sealed class StartMenuUI : BaseUI
    {
        public Action onStartButtonClick;
        public override void Cancel()
        {
            gameObject.SetActive(false);
        }

        public void OnStartClick()
        {
            onStartButtonClick?.Invoke();
        }
        public override void Execute()
        {
            gameObject.SetActive(true);
        }
    }
}
   
