
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider soundVolume;
    [SerializeField] private Button applyBtn;

    public void OnEnable()
    {
        musicVolume.value = PlayerPrefs.GetFloat("MusicVolume");
        soundVolume.value = PlayerPrefs.GetFloat("SoundVolume");
        
    }
    private void FixedUpdate()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("SoundVolume", soundVolume.value);
        AudioManager.Instance.ChangeMusicVolume();
        AudioManager.Instance.ChangeSoundVolume();
    }

    public void OnClickApplyBtn()
    {
        AudioManager.Instance.PlayAudioByType(AudioType.Button, AudioSubType.Sound);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("SoundVolume", soundVolume.value);
        gameObject.SetActive(false);
    }
}
