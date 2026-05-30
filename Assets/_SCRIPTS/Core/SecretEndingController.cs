using System.Collections;
using UnityEngine;

public class SecretEndingController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameLoopFlowController flowController;

    [Header("UI")]
    [SerializeField] private GameObject HUDCanvas;
    [SerializeField] private GameObject preFinalUI;// белая вспышка
    [SerializeField] private GameObject secretUI;// промежуточный UI

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip secretSound;

    [Header("Settings")]
    [SerializeField] private float flashFadeDuration = 2f;
    [SerializeField] private float secretDuration = 6f;

    [Header("Timer Dependency")]
    [SerializeField] private TimeScript timer;

    private bool activated;

    public void StartSecretEnding()
    {
        if (activated) return;

        activated = true;
        StartCoroutine(SecretEndingRoutine());
    }

    private IEnumerator SecretEndingRoutine()
    {
        if (timer != null)
            timer.StopTimer();
        if (HUDCanvas != null)
            HUDCanvas.SetActive(false);

        CanvasGroup flashGroup = null;

        secretUI.SetActive(true);
        if (preFinalUI != null)
        {
            preFinalUI.SetActive(true);

            flashGroup = preFinalUI.GetComponent<CanvasGroup>();

            if (flashGroup)
                flashGroup.alpha = 1f; // Делаем вспышку полностью белой
        }

        yield return new WaitForSeconds(1f);

        if (flashGroup != null)
        {
            float t = 0f;

            while (t < flashFadeDuration)
            {
                t += Time.deltaTime;

                flashGroup.alpha = Mathf.Lerp(
                    1f,
                    0f,
                    t / flashFadeDuration
                );

                yield return null;
            }

            flashGroup.alpha = 0f;
        }

        if (preFinalUI != null)
            preFinalUI.SetActive(false);

        if (secretUI != null)
            secretUI.SetActive(true);

        if (audioSource != null && secretSound != null)
        {
            audioSource.clip = secretSound;
            audioSource.Play();
        }

        yield return new WaitForSeconds(secretDuration);

        if (audioSource != null)
            audioSource.Stop();

        if (secretUI != null)
            secretUI.SetActive(false);

        if (flowController != null)
            flowController.TriggerSecretEnding();
    }
}