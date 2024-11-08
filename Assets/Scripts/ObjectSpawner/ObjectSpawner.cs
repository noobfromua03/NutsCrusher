﻿using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner
{
    private ObjectPool objectPool = new();
    private GameObject objectPrefab;
    private Transform container;
    private bool StopSpawning;
    private const int MAX_SPAWN_IN_ONE_WAVE = 3;

    public Action DestroyActiveNuts;
    public void Initialize(Transform container)
    {
        this.container = container;
        objectPrefab = ObjectConfig.Instance.ThrowObjectPrefab;
    }

    private ObjectData SpawnObject(ObjectType type)
    {
        var throwObject = objectPool.GetObjectByType(type);

        if (throwObject == null)
        {
            var newThrowObject = GameObject.Instantiate(objectPrefab, container);
            throwObject = newThrowObject.GetComponent<ObjectData>();

            var data = ObjectConfig.Instance.GetObjectByType(type);
            throwObject.Initialize(data);
            objectPool.AddToPool(throwObject);
        }
        else
            throwObject.gameObject.SetActive(true);

        return throwObject;
    }

    private void SpawnHandler()
    {
        for (int i = 0; i < Random.Range(0f, MAX_SPAWN_IN_ONE_WAVE); i++)
        {
            var type = ObjectConfig.Instance.GetRandomType();
            SpawnObject(type);
        }
    }

    public void MenuSpawnHandler()
    {
        for (int i = 0; i < Random.Range(0f, 4f); i++)
        {
            if (Random.value < 0.95f)
                SpawnObject(ObjectType.Nut);
            else
                SpawnObject(ObjectType.GoldenNut);
        }
    }

    public void SpawnAllPossibleVariants()
    {
        var allObjects = ObjectConfig.Instance.ThrowObjectData;

        for (int i = 0; i < allObjects.Count; i++)
        {
            var obj = SpawnObject(allObjects[i].Type);
            obj.gameObject.SetActive(false);
        }

        for (int i = allObjects.Count; i < objectPool.MinObjects; i++)
        {
            var type = ObjectConfig.Instance.GetRandomType();
            var obj = SpawnObject(type);
            obj.gameObject.SetActive(false);
        }
    }

    public IEnumerator SpawnObjectsCoroutine()
    {
        while (StopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(2f, 3f));
            SpawnHandler();
        }
    }

    public IEnumerator SpawnObjectsForMenuCoroutine()
    {
        DestroyActiveNuts += objectPool.CrashAllActiveNuts;

        while (StopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            MenuSpawnHandler();
        }
    }

    public void StopSpawn()
        => StopSpawning = true;
}