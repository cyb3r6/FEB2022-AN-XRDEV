using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit;

public class VignetteApplier : MonoBehaviour
{
    [Range(0, 1)]
    public float intensity = 0.5f;
    [Range(0, 10)]
    public float duration = 0.5f;
    public Volume postProcessVolume;
    public List<LocomotionProvider> locomotionProviders = new List<LocomotionProvider>();

    private Vignette vignette;
    private float elapsedTime = 0.0f;

    
    void Awake()
    {
        // get vignette
        if(postProcessVolume.profile.TryGet(out Vignette vignette))
        {
            this.vignette = vignette;
        }

        foreach( var locomotionProvider in locomotionProviders)
        {
            locomotionProvider.beginLocomotion += FadeIn;
            locomotionProvider.endLocomotion += FadeOut;
        }
    }

    private void OnDisable()
    {
        foreach (var locomotionProvider in locomotionProviders)
        {
            locomotionProvider.beginLocomotion -= FadeIn;
            locomotionProvider.endLocomotion -= FadeOut;
        }
    }

    public void FadeIn(LocomotionSystem locomotionSystem)
    {
        StartCoroutine(Fade(0, intensity));
    }
    public void FadeOut(LocomotionSystem locomotionSystem)
    {
        StartCoroutine(Fade(0, intensity));
    }

    private IEnumerator Fade(float startValue, float endValue)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;
            float fadeValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            ApplyValue(fadeValue);
            yield return new WaitForEndOfFrame();
        }
    }

    private void ApplyValue(float value)
    {
        // override the original intensity of the vignette
        vignette.intensity.Override(value);
    }
}
