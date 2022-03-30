using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public Renderer screen;

    [Range(0, 1)]
    public float alpha = 1.0f;
    [Range(0, 10)]
    public float duration = 0.5f;

    private float elapsedTime = 0.0f;
    private Material screenMaterial;

    
    void Start()
    {
        screenMaterial = screen.material;
    }

    public float FadeIn()
    {
        StartCoroutine(Fade(0, alpha));
        return duration;
    }

    public float FadeOut()
    {
        StartCoroutine(Fade(alpha, 0));
        return duration;
    }

    private IEnumerator Fade(float startValue, float endValue)
    {
        float elapsedTime = 0.0f;
        
        while (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;
            float fadeValue = Mathf.Lerp(startValue, endValue, elapsedTime/duration);
            ApplyValue(fadeValue);
            yield return new WaitForEndOfFrame();
        }
    }

    private void ApplyValue(float value)
    {
        Color fadeColor = new Color(0, 0, 0, value);
        screenMaterial.color = fadeColor;
    }
}
