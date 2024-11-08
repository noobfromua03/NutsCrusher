using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteController : MonoBehaviour
{
    private static VignetteController instance;
    public static VignetteController Instance { get => instance; }

    [SerializeField] private Volume volume;
    [field: SerializeField] private List<Color> colors;
    private Vignette vignette;
    private float timer = 0;
    private float duration = 2f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        volume.profile.TryGet<Vignette>(out vignette);
    }

    public IEnumerator VignetteAnimation(int colorIndex, float targetIntensity)
    {
        timer = 0;
        vignette.color.value = colors[colorIndex];
        var currentIntensity = vignette.intensity.value;

        while (timer <= duration)
        {
            timer += Time.deltaTime;

            var value = Mathf.Lerp(currentIntensity, targetIntensity, timer / duration);

            vignette.intensity.value = value;

            yield return null;
        }
    }
}
