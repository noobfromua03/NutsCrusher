using UnityEngine;

public class MenuController : MonoBehaviour
{
    private ObjectSpawner objectSpawner = new();
    private Coroutine spawnRoutine;
    private bool isNutsCrushed;

    [SerializeField] private DisableZone disableZone;
    [SerializeField] private BackgroundResizer menuBack;
    [SerializeField] private BackgroundResizer levelBack;


    private void Awake()
    {
        objectSpawner.Initialize(GameManager.CreateContainer("ObjectsContainer", transform));
        objectSpawner.SpawnAllPossibleVariants();
    }

    private void OnEnable()
    {
        isNutsCrushed = false;
        spawnRoutine = StartCoroutine(objectSpawner.SpawnObjectsForMenuCoroutine());
    }

    public void MovingBackgroundAtStartGame(float timer, float heightScreen)
    {
        if (isNutsCrushed == false)
        {
            objectSpawner.DestroyActiveNuts?.Invoke();
            StopCoroutine(spawnRoutine);
            isNutsCrushed = true;
        }

        var tempPos = menuBack.transform.position;
        tempPos.y = Mathf.Lerp(tempPos.y, heightScreen, timer);
        menuBack.transform.position = tempPos;
        tempPos = levelBack.transform.position;
        tempPos.y = Mathf.Lerp(tempPos.y, 0f, timer);
        levelBack.transform.position = tempPos;
    }
}
