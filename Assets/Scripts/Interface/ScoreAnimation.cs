using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentValue = 0;
    private int targetValue = 0;
    private float timer = 0f;
    private float animationDuration = 0.3f;
    private bool isAnimating = false;

    private Vector3 original = new Vector3(1f, 1f, 1f);

    public void UpdateScore(int score)
    {
        if (score != currentValue)
        {
            if (isAnimating)
                currentValue = targetValue;
            targetValue = score;
            timer = 0f;
            isAnimating = true;
        }
    }

    private void FixedUpdate()
    {
        if (isAnimating)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / animationDuration);

            int value = Mathf.FloorToInt(Mathf.Lerp(currentValue, targetValue, progress));

            scoreText.text = value.ToString();
            ScaleAnimation(progress);
            if (progress >= 1f)
            {
                currentValue = targetValue;
                isAnimating = false;
            }

        }
    }

    private void ScaleAnimation(float progress)
    {
        var scale = scoreText.transform.localScale;

        if (progress <= 0.5f)
            scale.y = Mathf.Lerp(original.y, original.y * 1.5f, progress);
        else
            scale.y = Mathf.Lerp(original.y * 1.5f, original.y, progress);

        scoreText.transform.localScale = scale;

    }
}