using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingTransition : MonoBehaviour
{
    public PostProcessVolume volume;
    private Bloom bloom;
    private ChromaticAberration chromaticAberration;
    public float fadeSpeed = 0.8f;

    public enum FadeDirection  
    {
        In, Out
    }

    private void Start() 
    {
        volume.profile.TryGetSettings(out bloom);
        volume.profile.TryGetSettings(out chromaticAberration);
        // vignette.smoothness.value 
        StartCoroutine(Transition(FadeDirection.In));
    }

    private IEnumerator Transition(FadeDirection fadeDirection) 
    {
        float fadeEndValue = (fadeDirection == FadeDirection.Out)? 0 : 10;
        float chromeEndValue = (fadeDirection == FadeDirection.Out)? 0.07f : 1;

        if (fadeDirection == FadeDirection.Out) 
        {
            while (bloom.intensity.value >= fadeEndValue) 
            {
                SetBloom(ref bloom.intensity.value, fadeDirection);
                SetChrom(ref chromaticAberration.intensity.value, fadeDirection);
                yield return null;
            }
        } else {
            print(bloom.intensity.value);
            while (bloom.intensity.value <= fadeEndValue) 
            {
                SetBloom(ref bloom.intensity.value, fadeDirection);
                SetChrom(ref chromaticAberration.intensity.value, fadeDirection);
                yield return null;
            }
        }
    }

    private void SetBloom(ref float bloomValue, FadeDirection fadeDirection) 
    {
        bloomValue += (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out)? -1 : 1);
    }

    private void SetChrom(ref float chromValue, FadeDirection fadeDirection) 
    {
        chromValue += (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out)? -1 : 1);
    }
}
