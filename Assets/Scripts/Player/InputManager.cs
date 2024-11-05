using UnityEngine;

public class InputManager
{
    private Vector2 touchPosition = new(0, -100);
    public Vector2 CurrentTouch { get => touchPosition; }

    public bool GetTouchPosition()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                return true;
            }
        }
        return false;
    }

    public void ClearTouchPosition()
        => touchPosition = new(0, -100);

}
