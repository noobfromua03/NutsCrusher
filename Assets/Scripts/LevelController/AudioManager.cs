using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get => instance; }

    [SerializeField] private AudioMixer audioMixer;

    private List<AudioData> audioClips = new();

    private List<AudioSource> audioSources = new();

    private const float SOUND_VOLUME = 0f;
    private const float MUSIC_VOLUME = -4f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayAudioByType(AudioType.Music, AudioSubType.Music);
        ChangeMusicVolume();
        ChangeSoundVolume();
    }

    public AudioSource PlayAudioByType(AudioType type, AudioSubType subType)
    {
        if (audioClips.Any(a => a.Type == type) == false)
            GetAudioData(type);

        var source = GetFreeAudioSource(type, subType);
        source.clip = GetClip(type);
        source.Play();
        return source;
    }
    private AudioSource GetFreeAudioSource(AudioType type, AudioSubType subType)
    {
        AudioSource freeSource = audioSources.Find(s => s.isPlaying == false);

        if (audioSources.Count == 0 || freeSource == null)
        {
            freeSource = this.AddComponent<AudioSource>();
            audioSources.Add(freeSource);
        }

        switch (subType)
        {
            case AudioSubType.Music:
                freeSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music").First();
                break;
            case AudioSubType.Sound:
                freeSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Sound").First();
                break;
        }

        audioSources.Add(freeSource);
        freeSource.loop = subType == AudioSubType.Music;
        freeSource.playOnAwake = false;

        return freeSource;
    }

    private void GetAudioData(AudioType type)
    {
        var data = AudioConfig.Instance.GetAudioDataByType(type);
        audioClips.Add(data);
    }

    private AudioClip GetClip(AudioType type)
        => audioClips.OrderBy(c => UnityEngine.Random.value).First(c => c.Type == type).Clip;

    public bool IsSoundPlaying(AudioType type)
    {
        bool exist = false;

        var clip = audioClips.Find(a => a.Type == type).Clip;

        if (clip != null)
        {
            foreach (var source in audioSources)
            {
                if (clip == source.clip)
                    exist = true;
            }
        }

        return exist;
    }

    public void Reload()
    {
        foreach (var source in audioSources)
            Destroy(source);
        audioSources.Clear();
    }

    public void ChangeSoundVolume()
    {
        
        if (PlayerPrefs.HasKey("SoundVolume"))
            audioMixer.SetFloat("SoundVolume", PlayerPrefs.GetFloat("SoundVolume"));
        else
            audioMixer.SetFloat("SoundVolume", -30f);
    }

    public void ChangeMusicVolume()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
            audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        else
            audioMixer.SetFloat("MusicVolume", -30f);
    }
}