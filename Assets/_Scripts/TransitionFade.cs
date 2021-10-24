using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TransitionFade : MonoBehaviour
{
    public Image fadeOutUIImage;
    public TextMeshProUGUI text;
    public float fadeSpeed = 0.8f;

    public enum FadeDirection 
    {
        In, Out
    }

    void OnEnable() 
    {
        StartCoroutine(Fade(FadeDirection.In));
    }

    private IEnumerator Fade(FadeDirection fadeDirection) 
    {
        float alpha = (fadeDirection == FadeDirection.Out)? 1 : 0;
        float fadeEndValue = (fadeDirection == FadeDirection.Out)? 0 : 1;

        if (fadeDirection == FadeDirection.Out) 
        {
            while (alpha >= fadeEndValue) 
            {
                SetColorImage (ref alpha, fadeDirection);
                yield return null;
            }
            fadeOutUIImage.enabled = false;
        } else {
            fadeOutUIImage.enabled = true;
            while (alpha <= fadeEndValue) 
            {
                SetColorImage(ref alpha, fadeDirection);
                yield return null;
            }
        }
    }

    public IEnumerator FadeAndLoadScene(FadeDirection fadeDirection, string sceneToLoad) 
    {
        yield return Fade(fadeDirection);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void SetColorImage(ref float alpha, FadeDirection fadeDirection) 
    {
        fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out)? -1 : 1);
    }
}
