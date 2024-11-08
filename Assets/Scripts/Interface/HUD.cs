
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    [SerializeField] private ScoreAnimation score;
    [SerializeField] private LifeAnimation lifes;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject TopBar;
    [SerializeField] private TextMeshProUGUI sessionScore;

    public Action Restart;
    public ScoreAnimation Score { get => score; }
    public LifeAnimation Lifes { get => lifes; }

    public void OnGameOver()
    {
        endGamePanel.SetActive(true);
        sessionScore.text = score.GetScore();
        TopBar.gameObject.SetActive(false);
    }

    public void RestartBtn()
    {
        AudioManager.Instance.PlayAudioByType(AudioType.Button, AudioSubType.Sound);
        Restart?.Invoke();
        GameManager.Instance.CreateLevelController();
        Destroy(gameObject);
    }

    public void ReturnToMenuBtn()
    {
        AudioManager.Instance.PlayAudioByType(AudioType.Button, AudioSubType.Sound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DestroySelf()
        => Destroy(gameObject);
}
