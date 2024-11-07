using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioConfig : AbstractConfig<AudioConfig>
{
    [field: SerializeField] public List<AudioData> Sounds { get; private set; }

    public AudioData GetAudioDataByType(AudioType type)
        => Sounds.OrderBy(s => UnityEngine.Random.value).First(s => s.Type == type);
}

[Serializable]

public class AudioData
{
    [field: SerializeField] public AudioType Type { get; private set; }
    [field: SerializeField] public AudioClip Clip { get; private set; }
}

public enum AudioType
{
    Almonds = 0,
    Hazelnuts = 1,
    Cashews = 2,
    Peanuts = 3,
    Walnuts = 4,
    Pecans = 5,
    Macadamias = 6,
    BrazilNuts = 7,
    GoldenNuts = 8,
    Stone = 9,
    Bomb = 10,
    CrashingNut = 11,
    Music = 12,
    EmptyTap = 13,
    Button = 14,
    GameOver = 15,
    NewRecord = 16
}

public enum AudioSubType
{
    Sound = 0,
    Music = 1
}