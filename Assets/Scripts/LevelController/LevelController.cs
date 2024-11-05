using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class LevelController : MonoBehaviour
{
    private TouchController touchController = new();
    private ObjectSpawner objectSpawner = new();
    private PlayerData playerData = new();

    private ScoreAnimation scoreAnimator;
    private LifeAnimation lifeAnimator;
    [SerializeField] private DisableZone disableZone;

    private void Awake()
    {
        objectSpawner.Initialize(GameManager.CreateContainer("ObjectsContainer", transform));
        objectSpawner.SpawnAllPossibleVariants();
        Initialize(GameManager.Instance.CreateHUD());
    }

    private void Start()
    {
        ActionSubscribes();
        StartCoroutine(objectSpawner.SpawnObjectsCoroutine());
    }

    private void Update()
    {
        touchController.TouchUpdate();
    }

    private void Initialize(HUD hud)
    {
        scoreAnimator = hud.Score;
        lifeAnimator = hud.Lifes;
    }

    private void ActionSubscribes()
    {
        touchController.BreakStreak += playerData.BreakStreak;

        ObjectData.RemoveLife += playerData.RemoveLife;
        ObjectData.AddScore += playerData.AddScore;
        ObjectData.GameOver += GameOver;

        disableZone.RemoveLife += playerData.RemoveLife;

        playerData.GameOver += GameOver;
        playerData.UpdateLifes += lifeAnimator.UpdateLifes;
        playerData.UpdateScore += scoreAnimator.UpdateScore;
    }

    private void Unsubscribe()
    {
        touchController.BreakStreak -= playerData.BreakStreak;

        ObjectData.RemoveLife -= playerData.RemoveLife;
        ObjectData.AddScore -= playerData.AddScore;
        ObjectData.GameOver -= GameOver;

        disableZone.RemoveLife -= playerData.RemoveLife;

        playerData.GameOver -= GameOver;
        playerData.UpdateLifes -= lifeAnimator.UpdateLifes;
        playerData.UpdateScore -= scoreAnimator.UpdateScore;
    }

    private void GameOver()
    {
        Unsubscribe();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
