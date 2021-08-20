using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Asteroids.Command;

public class ScoreOutput 
{
    private Text _scoreOutputText;


    public ScoreOutput(Text scoreOutput)
    {
        _scoreOutputText = scoreOutput;
    }

    public void ShowScore(int score)
    {
        _scoreOutputText.text = InterpretToKRecord(score);
    }

    private string InterpretToKRecord(int value)
    {
        int multiplier=0;
        while (value / 1000 > 0)
        {
            value /= 1000;
            multiplier++;
        }

        if (multiplier == 2)
            return value + "M";
        if (multiplier == 1)
            return value + "K";
        else
            return value +"";
    }
}
