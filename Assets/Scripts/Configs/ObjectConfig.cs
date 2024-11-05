using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectConfig : AbstractConfig<ObjectConfig>
{
    [field: SerializeField] public List<ThrowObjectData> ThrowObjectData { get; private set; }
    [SerializeField] public GameObject ThrowObjectPrefab;

    public ThrowObjectData GetObjectByType(ObjectType type)
        => ThrowObjectData.Where(o => o.Type == type).OrderBy(o => UnityEngine.Random.value).First();
    public ObjectType GetRandomType()
        => ThrowObjectData.OrderBy(o => UnityEngine.Random.value).First().Type;
}

[Serializable]
public class ThrowObjectData
{
    [field: SerializeField] public ObjectType Type { get; private set; }
    [field: SerializeField] public int Taps { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public AudioType AudioType { get; private set; }
}

public enum ObjectType
{
    Nut = 0,
    Stone = 1,
    Bomb = 2,
    GoldenNut = 3
}