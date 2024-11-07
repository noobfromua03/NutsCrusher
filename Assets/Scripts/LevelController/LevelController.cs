using UnityEngine;

public class LevelController : MonoBehaviour
{
    private TouchController touchController = new();
    private ObjectSpawner objectSpawner = new();
    private PlayerData playerData = new();
    private HUD hud;
    private Coroutine VignetteRoutine;

    [SerializeField] private DisableZone disableZone;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private ParticleSystem newRecordParticle;

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
        playerData.StreakAnimation += StreakAnimation;
        playerData.BreakStreakAnimation += BreakStreakAnimation;

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
        playerData.StreakAnimation -= StreakAnimation;
        playerData.BreakStreakAnimation -= BreakStreakAnimation;

        hud.restart -= Restart;
    }

    private void EmptyTap(Vector2 pos)
    {
        particle.gameObject.transform.position = pos;
        particle.Play();
        AudioManager.Instance.PlayAudioByType(AudioType.EmptyTap, AudioSubType.Sound);
    }

    public void StreakAnimation(int streak)
    {
        if (VignetteRoutine != null)
            StopCoroutine(VignetteRoutine);

        if (streak >= 15)
            VignetteRoutine = StartCoroutine(VignetteController.Instance.VignetteAnimation(0, 0.7f));
        else if (streak >= 5)
            VignetteRoutine = StartCoroutine(VignetteController.Instance.VignetteAnimation(0, 0.3f));
    }

    public void BreakStreakAnimation()
    {
        if (VignetteRoutine != null)
            StopCoroutine(VignetteRoutine);
        VignetteRoutine = StartCoroutine(VignetteController.Instance.VignetteAnimation(0, 0));
    }


    private void Restart()
    {
        Unsubscribe();
        playerData.ChangeBestScore();
    }

    private void GameOver()
    {
        if (VignetteRoutine != null)
            StopCoroutine(VignetteRoutine);
        objectSpawner.StopSpawn();
        VignetteRoutine = StartCoroutine(VignetteController.Instance.VignetteAnimation(1, 0.5f));
        Unsubscribe();

        if(playerData.ChangeBestScore())
        {
            newRecordParticle.Play();
            AudioManager.Instance.PlayAudioByType(AudioType.NewRecord, AudioSubType.Sound);
        }
        else
            AudioManager.Instance.PlayAudioByType(AudioType.GameOver, AudioSubType.Sound);

        hud.OnGameOver();
    }
}
