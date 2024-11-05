using UnityEngine;

public class BackgroundResizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public bool MoveDown;
    Vector3 startPos = new(0f, 0f, 0f);
    private void Start()
    {
        transform.localPosition = startPos;

        float spriteWidth = spriteRenderer.sprite.bounds.size.x;
        float spriteHeight = spriteRenderer.sprite.bounds.size.y;

        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        Vector3 scale = transform.localScale;
        scale.x = screenWidth / spriteWidth;
        scale.y = screenHeight / spriteHeight;
        transform.localScale = scale;

        if(MoveDown)
        {
            var pos = transform.position;
            pos.y -= screenHeight;
            transform.position = pos;
        }    
    }


}
