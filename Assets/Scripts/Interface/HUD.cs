
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private ScoreAnimation score;
    [SerializeField] private LifeAnimation lifes;
    //[SerializeField] private BestScoreAnimation bestScore;

    public ScoreAnimation Score { get => score; }
    public LifeAnimation Lifes { get => lifes; }

    public void DestroySelf()
        => Destroy(gameObject);
}
