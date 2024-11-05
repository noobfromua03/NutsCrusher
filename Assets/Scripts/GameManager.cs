using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    [SerializeField] private GameObject prefabHUD;
    [SerializeField] private GameObject prefabLevelController;

    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private MenuController menuController;

    private void Awake()
    {
        instance = this;

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
        var levelController = Instantiate(prefabLevelController);
        levelController.name = "LevelController";
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
