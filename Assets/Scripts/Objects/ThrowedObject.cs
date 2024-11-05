using UnityEngine;
using Random = UnityEngine.Random;

public class ThrowedObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private float rotatePower;
    private Vector2 throwVector;

    private Vector2 baseForcePower = new(12f, 16f);
    private Vector2 baseRotatePower = new(0.5f, 5f);
    private float baseSideForce = 4f;
    private float halfScreenSize = 0f;
    private float spawnYPos = -7.5f;

    private void Awake()
    {
        halfScreenSize = Camera.main.orthographicSize * Camera.main.aspect;
    }
    private void OnEnable()
    {
        transform.position = SetStartPosition();
        throwVector = SetThrowVector();
        rotatePower = SetPotatePower();
        ForceUp();
    }

    private void FixedUpdate()
    {
        Rotate();
    }
    public void ForceUp()
        => rb.AddForce(throwVector, ForceMode2D.Impulse);

    public Vector2 SetThrowVector()
        => new(Random.Range(-baseSideForce + transform.position.x / -2.5f, baseSideForce - transform.position.x / 2.5f), 
            Random.Range(baseForcePower.x, baseForcePower.y));

    public void Rotate()
        => transform.Rotate(Vector3.forward, rotatePower);

    public float SetPotatePower()
        => Random.value > 0.5f ? Random.Range(baseRotatePower.x, baseRotatePower.y) : 
        Random.Range(baseRotatePower.x, baseRotatePower.y) * -1f;

    public Vector2 SetStartPosition()
    {
        float x = Random.Range(halfScreenSize * -1f, halfScreenSize);
        return new (x, spawnYPos);
    }
}
