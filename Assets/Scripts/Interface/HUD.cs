
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    [SerializeField] private ScoreAnimation score;
    [SerializeField] private LifeAnimation lifes;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TextMeshProUGUI sessionScore;

    public Action restart;
    public ScoreAnimation Score { get => score; }
    public LifeAnimation Lifes { get => lifes; }

    public void OnGameOver()
    {
        endGamePanel.SetActive(true);
        sessionScore.text = "Score:" + score.GetScore();
    }

    public void RestartBtn()
    {
        restart?.Invoke();
        Destroy(gameObject);
        GameManager.Instance.CreateLevelController();
    }

    public void ReturnToMenuBtn()
        => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void DestroySelf()
        => Destroy(gameObject);
}
