using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeAnimation : MonoBehaviour
{
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;

    private List<Image> lifes = new();

    private int currentLifeIcon = 0;
    private float animationDuration = 0.3f;

    private Vector3 original = new Vector3(1f, 1f, 1f);

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
            lifes.Add(CreateLifeIcon());
    }

    private Image CreateLifeIcon()
    {
        var lifeIcon = new GameObject("LifeIcon");
        lifeIcon.transform.parent = transform;
        var lifeImage = lifeIcon.AddComponent<Image>();
        lifeImage.sprite = sprite1;
        lifeImage.preserveAspect = true;
        lifeIcon.transform.localScale = original;
        return lifeIcon.GetComponent<Image>();
    }
    public void UpdateLifes()
    {
        StartCoroutine(CoroutineIcon(currentLifeIcon, 0, true));
        currentLifeIcon++;
    }

    private IEnumerator CoroutineIcon(int iconIndex, float timer, bool isAnimating)
    {

        while (isAnimating)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / animationDuration);

            ScaleAnimation(progress, iconIndex);
            if (progress >= 1f)
            {
                lifes[iconIndex].sprite = sprite2;
                isAnimating = false;
            }
            yield return null;
        }
    }

    private void ScaleAnimation(float progress, int iconIndex)
    {
        var scale = transform.localScale;

        if (progress <= 0.5f)
            scale.y = Mathf.Lerp(original.y, original.y * 2f, progress);
        else
            scale.y = Mathf.Lerp(original.y * 2f, original.y, progress);

        lifes[iconIndex].transform.localScale = scale;
    }
}