using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Rendering; // Обязательно для работы с Volume

public class WinSequenceController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameLoopFlowController flowController;

    [Header("Settings")]
    [SerializeField] private GameObject winDoor;

    [Header("Video & UI Settings")]
    [SerializeField] private VideoPlayer winVideoPlayer;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private AudioSource backgroundMusic;

    [Header("Visual Settings")]
    [SerializeField] private Volume globalVolume; // Ссылка на твой Global Volume

    private void OnEnable()
    {
        if (winVideoPlayer != null)
        {
            winVideoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    private void OnDisable()
    {
        if (winVideoPlayer != null)
        {
            winVideoPlayer.loopPointReached -= OnVideoFinished;
        }
    }

    public void ShowDoor()
    {
        if (winDoor != null) winDoor.SetActive(true);
    }

    public void StartWinFlow()
    {
        StartCoroutine(PrepareAndPlayRoutine());
    }

    private IEnumerator PrepareAndPlayRoutine()
    {
        if (winVideoPlayer == null)
        {
            FinalizeWin();
            yield break;
        }

        // 1. Скрываем UI, паузу и отключаем эффекты
        if (gameplayUI != null) gameplayUI.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);

        // Отключаем Global Volume, чтобы картинка видео была чистой
        if (globalVolume != null) globalVolume.enabled = false;

        if (backgroundMusic != null) backgroundMusic.Pause();

        // 2. Запуск видео
        winVideoPlayer.gameObject.SetActive(true);
        winVideoPlayer.Play();

        yield return null;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // 3. Выключаем видео
        vp.Stop();
        vp.gameObject.SetActive(false);

        // Если нужно вернуть эффекты для экрана победы, можно включить здесь:
        // if (globalVolume != null) globalVolume.enabled = true;

        FinalizeWin();
    }

    private void FinalizeWin()
    {
        if (flowController != null)
        {
            flowController.RequestWinFromExit();
        }
    }
}