using Asteroids.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Asteroids.Canvas
{
    public class CanvasController : MonoBehaviour
    {

        [SerializeField] private ScoreUI _scoreUI;
        [SerializeField] private RestartUI _restartUI;
        [SerializeField] private StartMenuUI _startMenuUI;
        private readonly Stack<StateUI> _stateUi = new Stack<StateUI>();
        private BaseUI _currentWindow;

        public Action OnRestartButtonClick;
        public Action OnStartButtonClick;
        private void Awake()
        {
            Execute(StateUI.StartMenuUI);
            _startMenuUI.onStartButtonClick += OnStartClick;
            _restartUI.Cancel();
            _restartUI.onRestartButtonClick += OnRestartClick;
            _scoreUI.Cancel();
        }
        public void ShowScore(int point)
        {
            _scoreUI.ShowScore(point);
        }
      
        public void GameEnd()
        {
            Debug.Log("GameEnd");
            Execute(StateUI.RestartUI);
        }

        private void OnRestartClick()
        {
            OnRestartButtonClick?.Invoke();
            Execute(StateUI.None, false);
        }

        private void OnStartClick()
        {
            Execute(StateUI.ScoreUI, false);
            OnStartButtonClick?.Invoke();
        }


        private void Execute(StateUI stateUI, bool isSave = true)
        {
            Debug.Log(_currentWindow);
            Debug.Log(_currentWindow!=null);

            if (_currentWindow != null)
            {
                _currentWindow.Cancel();
                _currentWindow = null;
            }

            switch (stateUI)
            {
                case StateUI.RestartUI:
                    _currentWindow = _restartUI;
                    break;
                case StateUI.StartMenuUI:
                    _currentWindow = _startMenuUI;
                    break;
                case StateUI.ScoreUI:
                     _scoreUI.Execute();
                    break;
                default:
                    break;
            }
            if (_currentWindow != null)
            {
                _currentWindow.Execute();
            }
            if (isSave)
            {
                _stateUi.Push(stateUI);
            }
        }


    }
}
   
