using System;
using System.Linq;
using UnityEngine;

public class TouchController
{
    private InputManager input = new();

    private const float OVERLAP_RADIUS = 0.2f;

    public Action BreakStreak;
    public Action<Vector2> EmptyTap;

    public void TouchUpdate()
    {
        if (input.GetTouchPosition())
        {
            var currentObjectData = TouchOverlap();
            currentObjectData?.OnTapHandler();
            if (currentObjectData == null)
            {
                BreakStreak?.Invoke();
                EmptyTap?.Invoke(input.CurrentTouch);
            }
            input.ClearTouchPosition();
        }
    }

    private ObjectData TouchOverlap()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(input.CurrentTouch, OVERLAP_RADIUS);

        if (hits.Length > 0)
        {
            hits = hits.OrderByDescending(o => o.GetComponent<ObjectData>().ColliderOrderNumber).ToArray();

            return hits.First().gameObject.GetComponent<ObjectData>();
        }
        return null;
    }

    
}
