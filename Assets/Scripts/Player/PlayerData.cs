using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData
{
    private int Lifes = 3;
    private int Score = 0;
    private int Streak = 0;

    public Action GameOver;
    public Action UpdateLifes;
    public Action<int> UpdateScore;
    public Action<int> StreakAnimation;
    public Action BreakStreakAnimation;

    public void RemoveLife()
    {
        Lifes -= 1;
        BreakStreak();
        UpdateLifes?.Invoke();

        if (Lifes <= 0)
            GameOver?.Invoke();
    }

    public void AddScore(int value)
    {
        if (Streak >= 15)
            value += 2;
        else if (Streak >= 5)
            value += 1;

        Score += value;
        Streak++;
        UpdateScore?.Invoke(Score);

        if (Streak >= 5)
            StreakAnimation?.Invoke(Streak);
    }

    public void BreakStreak()
    {
        Streak = 0;
        BreakStreakAnimation?.Invoke();
    }

    public bool ChangeBestScore()
    {
        if (PlayerPrefs.GetInt("BestScore") < Score)
        {
            PlayerPrefs.SetInt("BestScore", Score);
            return true;
        }
        return false;
    }
}
