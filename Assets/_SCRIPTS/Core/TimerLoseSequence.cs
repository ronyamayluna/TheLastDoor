using System.Collections;
using UnityEngine;
using TMPro;
using Unity.Cinemachine;

public class TimerLoseSequence : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TimeScript timer;
    [SerializeField] private GameLoopFlowController flowController;
    [SerializeField] private CinemachineCamera vCam;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI[] blinkingTexts;
    [SerializeField] private GameObject preFinalUI;
    [SerializeField] private GameObject intermediateLoseUI;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip alarmSound;
    [SerializeField] private AudioClip finalLoseSound;

    [Header("Shake Settings")]
    [SerializeField] private float shakeIntensity = 2.0f;
    [SerializeField] private float shakeFrequency = 2.0f;

    private CinemachineBasicMultiChannelPerlin _noise;

    private void Awake()
    {
        if (vCam != null)
        {
            _noise = vCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
            if (_noise != null)
            {
                _noise.AmplitudeGain = 0f;
                _noise.FrequencyGain = 0f;
            }
        }
    }

    private void OnEnable() => timer.OnTimerEnd += StartLoseSequence;
    private void OnDisable() => timer.OnTimerEnd -= StartLoseSequence;

    private void StartLoseSequence() => StartCoroutine(LoseRoutine());

    private IEnumerator LoseRoutine()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)
            yield break;

        if (_noise != null) { _noise.AmplitudeGain = shakeIntensity; _noise.FrequencyGain = shakeFrequency; }
        if (audioSource && alarmSound) { audioSource.clip = alarmSound; audioSource.loop = true; audioSource.Play(); }

        float elapsed = 0f;
        while (elapsed < 5f)
        {
            float alpha = Mathf.PingPong(Time.time * 6f, 1f);
            foreach (var txt in blinkingTexts) if (txt) txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (_noise != null) _noise.AmplitudeGain = 0f;
        if (audioSource) audioSource.Stop();
        foreach (var txt in blinkingTexts) if (txt) txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 0);

        CanvasGroup flashGroup = null;
        if (preFinalUI)
        {
            preFinalUI.SetActive(true);
            flashGroup = preFinalUI.GetComponent<CanvasGroup>();
            if (flashGroup) flashGroup.alpha = 1f; // Делаем вспышку полностью белой
        }

        if (intermediateLoseUI) intermediateLoseUI.SetActive(true);
        if (audioSource && finalLoseSound) audioSource.PlayOneShot(finalLoseSound);

        yield return new WaitForSeconds(1f);

        // ПЛАВНОЕ ИСЧЕЗНОВЕНИЕ ВСПЫШКИ (Fade Out)
        if (flashGroup != null)
        {
            float fadeDuration = 2.0f; // Длительность таяния вспышки
            float t = 0;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                flashGroup.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
                yield return null;
            }
            flashGroup.alpha = 0f;
        }

        if (preFinalUI) preFinalUI.SetActive(false);

        yield return new WaitForSeconds(3.0f); // Сколько времени видна цитата после исчезновения вспышки
        if (intermediateLoseUI) intermediateLoseUI.SetActive(false);

        if (flowController != null) flowController.HandlePlayerDeath();
    }
}
