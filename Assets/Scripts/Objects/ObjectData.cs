using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ObjectData : MonoBehaviour
{
    static public int SpriteSortOrderCounter = 1;

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private ParticleSystem particles;

    public static Action RemoveLife;
    public static Action<int> AddScore;
    public static Action GameOver;

    public int ColliderOrderNumber { get; private set; }
    public ObjectType ObjectType { get; private set; }

    private int totalTaps;
    private int currentTaps;
    private AudioType audioType;
    private void OnEnable()
    {
        SetColliderOrder();
        currentTaps = totalTaps;
        circleCollider.enabled = true;
        sprite.gameObject.SetActive(true);
    }
    public void Initialize(ThrowObjectData data)
    {
        sprite.sprite = data.Image;
        totalTaps = data.Taps;
        ObjectType = data.Type;
        currentTaps = totalTaps;
        audioType = data.AudioType;
        ChangeParticleSystemMaterial();
    }

    public void OnTapHandler()
    {
        currentTaps -= 1;

        AudioManager.Instance.PlayAudioByType(audioType, AudioSubType.Sound);

        if (currentTaps <= 0)
        {
            Disable();
        }

        particles.Play();
    }

    private void SetColliderOrder()
    {
        ColliderOrderNumber = circleCollider.layerOverridePriority;
        circleCollider.layerOverridePriority = SpriteSortOrderCounter;
        SpriteSortOrderCounter++;
    }

    private void ChangeParticleSystemMaterial()
    {
        var particleSystemRenderer = particles.GetComponent<ParticleSystemRenderer>();
        particleSystemRenderer.material.mainTexture = sprite.sprite.texture;
        ChangePatriclesAmount(5);
    }

    private void ChangePatriclesAmount(int value)
    {
        var emission = particles.emission;
        var burst = emission.GetBurst(0);
        burst.count = value;
        emission.SetBurst(0, burst);
    }

    private void ChangeParticlesStartSpeed(int value)
    {
        var startSpeed = particles.main;
        startSpeed.startSpeed = value;
    }

    public void Disable()
    {
        switch (ObjectType)
        {
            case ObjectType.Nut:
                AudioManager.Instance.PlayAudioByType(AudioType.CrashingNut, AudioSubType.Sound);
                AddScore?.Invoke(totalTaps);
                break;
            case ObjectType.Stone:
                RemoveLife?.Invoke();
                break;
            case ObjectType.Bomb:
                GameOver?.Invoke();
                break;
            case ObjectType.GoldenNut:
                AddScore?.Invoke(10);
                break;
        }

        ChangePatriclesAmount(UnityEngine.Random.Range(15, 31));
        ChangeParticlesStartSpeed(20);
        particles.Play();
        circleCollider.enabled = false;
        sprite.gameObject.SetActive(false);


        if (particles.isPlaying == false)
            gameObject.SetActive(false);
    }
}