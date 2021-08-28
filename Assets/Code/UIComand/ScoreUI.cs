using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Command
{
    internal sealed class ScoreUI : BaseUI
    {
        [SerializeField] private Text _scoreText;
        private ScoreOutput _scoreOutput;

        public void ShowScore(int score)
        {
            _scoreOutput.ShowScore(score);
        }

        public override void Cancel()
        {
            gameObject.SetActive(false);
        }

        public override void Execute()
        {
            gameObject.SetActive(true);
            _scoreOutput = new ScoreOutput(_scoreText);
        }
    }
}
    
