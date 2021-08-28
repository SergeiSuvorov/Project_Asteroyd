using Asteroids.Canvas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
    public class ScoreCounter
    {
        private int _score = 0;
        private CanvasController _canvasController;

        public ScoreCounter(CanvasController canvasController)
        {
            _canvasController = canvasController;
        }

        public void AddScore(int point)
        {
            _score += point;
            _canvasController.ShowScore(_score);
        }

        public void ResetToZero()
        {
            _score = 0;
            _canvasController.ShowScore(_score);
        }
    }

}
    
