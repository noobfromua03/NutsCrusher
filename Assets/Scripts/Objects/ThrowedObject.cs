using UnityEngine;
using Random = UnityEngine.Random;

public class ThrowedObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private float forcePowerMin = 12f;
    private float forcePowerMax = 20f;
    private float rotatePowerMin = 0.5f;
    private float rotatePowerMax = 5f;

    private float rotatePower;
    private Vector2 throwVector;

    private void Start()
    {
        transform.position = SetStartPosition();
        throwVector = SetThrowVector();
        rotatePower = SetPotatePower();
        ForceUp();
        SetStartPosition();
    }

    private void FixedUpdate()
    {
        Rotate();
    }
    public void ForceUp()
        => rb.AddForce(throwVector, ForceMode2D.Impulse);

    public Vector2 SetThrowVector()
        => new(Random.Range(-2f, 2f), 1 * Random.Range(forcePowerMin, forcePowerMax));

    public void Rotate()
        => transform.Rotate(Vector3.forward, rotatePower);

    public float SetPotatePower()
        => Random.value > 0.5f ? Random.Range(rotatePowerMin, rotatePowerMax) : Random.Range(rotatePowerMin, rotatePowerMax) * -1f;

    public Vector2 SetStartPosition()
    {
        float halfScreenSize = Camera.main.orthographicSize * Camera.main.aspect - 3f;
        float x = Random.Range(halfScreenSize * -1f, halfScreenSize);
        return new (x, -7.5f);
    }
}
