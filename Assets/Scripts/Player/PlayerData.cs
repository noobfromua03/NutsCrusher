using System;

public class PlayerData
{
    private int Lifes = 3;
    private int Score = 0;
    private int Streak = 0;

    public Action GameOver;
    public Action UpdateLifes;
    public Action<int> UpdateScore;

    public void RemoveLife()
    {
        UpdateLifes?.Invoke();

        Lifes -= 1;
        BreakStreak();

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
    }

    public void BreakStreak()
        => Streak = 0;

}
