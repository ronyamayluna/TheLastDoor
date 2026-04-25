using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public Slider loadingSlider;
    public float duration = 3f;

    void Start()
    {
        StartCoroutine(AnimateLoadingBar());
    }

    IEnumerator AnimateLoadingBar()
    {
        float elapsedTime = 0f;
        loadingSlider.value = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            loadingSlider.value = Mathf.Clamp01(elapsedTime / duration);

            yield return null;
        }

        loadingSlider.value = 1f;

    }
}
