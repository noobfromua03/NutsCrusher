using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }
    private void SpawnObject()
    {
        //намагається взяти з пула. якщо немає тоді наступне

        Instantiate(prefab, transform);
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            SpawnObject();
        }
    }
}
