using System;
using UnityEngine;

public class DisableZone : MonoBehaviour
{
    public Action RemoveLife;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        if (collision.gameObject.TryGetComponent<ObjectData>(out var obj))
            if (obj.ObjectType == ObjectType.Nut || obj.ObjectType == ObjectType.GoldenNut)
                RemoveLife?.Invoke();
    }
}
