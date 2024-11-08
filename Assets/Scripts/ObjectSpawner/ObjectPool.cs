
using System.Collections.Generic;
using System.Linq;

public class ObjectPool
{
    public Dictionary<ObjectType, List<ObjectData>> poolObjects = new();

    private int counter = 0;
    private const int MIN_OBJECTS_AMOUNT = 30;
    public int MinObjects { get => MIN_OBJECTS_AMOUNT; }
    public ObjectData GetObjectByType(ObjectType type)
    {

        if (poolObjects.ContainsKey(type) && counter >= MIN_OBJECTS_AMOUNT)
            return poolObjects[type].Where(o => o.gameObject.activeSelf == false)
                .OrderBy(o => UnityEngine.Random.value)
                .FirstOrDefault();

        counter++;
        return null;
    }

    public void AddToPool(ObjectData data)
    {
        if (poolObjects.ContainsKey(data.ObjectType) == false)
            poolObjects.Add(data.ObjectType, new());

        poolObjects[data.ObjectType].Add(data);
    }

    public void CrashAllActiveNuts()
    {
        foreach(var type in poolObjects.Keys)
        {
            foreach(var nut in poolObjects[type])
            {
                if (nut.gameObject.activeSelf)
                    nut.Disable();
            }
        }
    }
}
