using UnityEngine;

public class LevelController : MonoBehaviour
{
    private TouchController touchController = new();
    private ObjectSpawner objectSpawner = new();
    private PlayerData playerData = new();
    private HUD hud;

    [SerializeField] private DisableZone disableZone;
    [SerializeField] private ParticleSystem particle;

    private void Awake()
    {
        objectSpawner.Initialize(GameManager.CreateContainer("ObjectsContainer", transform));
        objectSpawner.SpawnAllPossibleVariants();
        hud = GameManager.Instance.CreateHUD();
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

    private void ActionSubscribes()
    {
        touchController.BreakStreak += playerData.BreakStreak;
        touchController.EmptyTap += EmptyTap;

        ObjectData.RemoveLife += playerData.RemoveLife;
        ObjectData.AddScore += playerData.AddScore;
        ObjectData.GameOver += GameOver;

        disableZone.RemoveLife += playerData.RemoveLife;

        playerData.GameOver += GameOver;
        playerData.UpdateLifes += hud.Lifes.UpdateLifes;
        playerData.UpdateScore += hud.Score.UpdateScore;

        hud.restart += Restart;
    }

    private void Unsubscribe()
    {
        touchController.BreakStreak -= playerData.BreakStreak;
        touchController.EmptyTap -= EmptyTap;

        ObjectData.RemoveLife -= playerData.RemoveLife;
        ObjectData.AddScore -= playerData.AddScore;
        ObjectData.GameOver -= GameOver;

        disableZone.RemoveLife -= playerData.RemoveLife;

        playerData.GameOver -= GameOver;
        playerData.UpdateLifes -= hud.Lifes.UpdateLifes;
        playerData.UpdateScore -= hud.Score.UpdateScore;

        hud.restart -= Restart;
    }

    private void EmptyTap(Vector2 pos)
    {
        particle.gameObject.transform.position = pos;
        particle.Play();
    }

    private void Restart()
    {
        Unsubscribe();
        Destroy(gameObject);
    }

    private void GameOver()
    {
        Unsubscribe();
        hud.OnGameOver();
        Destroy(gameObject);
    }
}
