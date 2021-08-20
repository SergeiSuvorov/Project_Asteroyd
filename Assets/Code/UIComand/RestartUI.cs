
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Command
{
    internal sealed class RestartUI : BaseUI
    {
        public Action onRestartButtonClick;
        public Action onCancleButtonClick;
        public override void Cancel()
        {
            gameObject.SetActive(false);
        }

        public void OnRestartClick()
        {
            onRestartButtonClick?.Invoke();
        }
        public override void Execute()
        {
            gameObject.SetActive(true);
        }
    }
}
