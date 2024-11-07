using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    [SerializeField] private GameObject prefabHUD;
    [SerializeField] private GameObject prefabLevelController;

    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private MenuController menuController;

    private LevelController levelController;

    private void Awake()
    {
        instance = this;
#if UNITY_EDITOR
        //PlayerPrefs.SetInt("BestScore", 1); // delete this
#endif
        mainMenu.MoveBackground += menuController.MovingBackgroundAtStartGame;
    }

    public HUD CreateHUD()
    {
        var hud = Instantiate(prefabHUD);
        hud.name = "HUD";
        return hud.GetComponent<HUD>();
    }

    public void CreateLevelController()
    {
        if(levelController != null)
            Destroy(levelController.gameObject);
        levelController = Instantiate(prefabLevelController).GetComponent<LevelController>();
        levelController.gameObject.name = "LevelController";
    }

    public static Transform CreateContainer(string name, Transform transform)
    {
        GameObject container = new GameObject(name);
        container.transform.parent = transform;
        return container.transform;
    }

    public void DisableMenu()
    {
        mainMenu.gameObject.SetActive(false);
        menuController.gameObject.SetActive(false);
    }
}
