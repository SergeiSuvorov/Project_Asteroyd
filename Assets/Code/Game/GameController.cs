using Asteroids.Canvas;
using Asteroids.Chain_of_Responsibility;
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
        [SerializeField] private CanvasController _canvasController;

        private PlayerController _playerController;
        private ListExecuteObject _listExecuteObject;
        private Input _inputController;
        private AsterroidManager _asterroidManager;
        private bool _isGaming;
        private EnemyManagerModifire _enemyManagerModifire;
        private ScoreCounter _scoreCounter;
        public ListExecuteObject ListExecuteObject { get { return _listExecuteObject; } }
        public Transform PlayerTransform => _shipGameObject.transform;
        void Awake()
        {
            SubscribeToCanvasEvents();
        }

        public void CreateGame()
        {
            GameStarter gameStarter = new GameStarter();
            gameStarter.CreateGame(_bulletGameObject, _shipGameObject, _shipData, _barrel, _weaponData, this, out _playerController, out _listExecuteObject, out _inputController, out _asterroidManager);
            SubscribeToEvents();
            CreateEnemysChain();
            _scoreCounter = new ScoreCounter(_canvasController);
            _isGaming = true;
        }

        private void CreateEnemysChain()
        {
            _enemyManagerModifire = new EnemyManagerModifire(_asterroidManager);
            _enemyManagerModifire.Add(new AddEnemyModifire(_asterroidManager, 5));
            _enemyManagerModifire.Add(new AddEnemyModifire(_asterroidManager, 20));
            _enemyManagerModifire.Add(new AddEnemyModifire(_asterroidManager, 1));
            _enemyManagerModifire.Handle();
        }

        private void SubscribeToEvents()
        {
            _asterroidManager.AddPoint+= AddScore;
            _playerController.ShipDestroy += OnShipDestroy;
           
        }

        private void SubscribeToCanvasEvents()
        {
            _canvasController.OnStartButtonClick += CreateGame;
            _canvasController.OnRestartButtonClick += RestartGame;
        }

        private void UnsubscribeFromEvent()
        {
            _asterroidManager.AddPoint -= AddScore;
            _playerController.ShipDestroy -= OnShipDestroy;
        }
        private void AddScore(int score)
        {
            if (!_isGaming)
                return;

            _scoreCounter.AddScore(score);
            
        }

        public void OnShipDestroy()
        {
            GameEnd();
        }

        private void RestartGame()
        {
            _isGaming = true;
            _playerController.OnRestartGame();
            _asterroidManager.OnRestartGame();
            _scoreCounter.ResetToZero();
            SubscribeToEvents();
            CreateEnemysChain();
        }

        private void GameEnd()
        {
            _isGaming = false;
            UnsubscribeFromEvent();
            _asterroidManager.OnPlayerDie();
            _canvasController.GameEnd();
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
            if (!_isGaming)
                return;

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
