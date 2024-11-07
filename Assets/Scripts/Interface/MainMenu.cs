using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private GameObject settingsPanel;

    private float Timer = 0;
    public Action<float, float> MoveBackground;
    public Action DestroyAllNuts;

    private void Start()
    {

    }

    private void OnEnable()
    {
        startBtn.gameObject.GetComponent<Image>().fillAmount = 1;
    }

    public void OnClickStartBtn()
    {
        StartCoroutine(StartBtnAnimation());
    }

    public void OnClickSettingsBtn()
    {
        settingsPanel.SetActive(true);
    }

    private IEnumerator StartBtnAnimation()
    {
        Timer = 0;
        var fillAmount = startBtn.gameObject.GetComponent<Image>();

        while (Timer <= 1f)
        {
            Timer += Time.deltaTime;
            fillAmount.fillAmount = Mathf.Lerp(1f, 0f, Timer);
            MoveBackground?.Invoke(Timer, Camera.main.orthographicSize * 2f);
            yield return null;  
        }
        startBtn.gameObject.GetComponent<Image>().fillAmount = 0f;

        GameManager.Instance.DisableMenu();
        GameManager.Instance.CreateLevelController();
    }
}
