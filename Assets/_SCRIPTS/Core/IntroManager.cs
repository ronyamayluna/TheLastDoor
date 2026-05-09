using UnityEngine;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    [Header("Настройки видео")]
    public VideoPlayer videoPlayer;
    public GameObject videoUI; // Твои игровые UI, которые надо Скрыть на время видео

    [Header("Ссылки на игрока")]
    public PlayerController playerController;

    void Start()
    {
        // --- ДЛЯ ТЕСТОВ: Видео будет играть всегда ---
        StartIntro();

        /* 
        // --- ДЛЯ ФИНАЛЬНОЙ ИГРЫ: Раскомментируй это, а StartIntro() выше удали ---
        int hasSeenIntro = PlayerPrefs.GetInt("HasSeenIntro", 0);

        if (hasSeenIntro == 0)
        {
            StartIntro();
        }
        else
        {
            SkipIntro();
        }
        */
    }

    void StartIntro()
    {
        Time.timeScale = 0f;

        if (playerController != null)
            playerController.enabled = false;

        // ВЫКЛЮЧАЕМ лишние UI
        if (videoUI != null)
            videoUI.SetActive(false);

        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        EndIntro();
    }

    public void EndIntro()
    {
        PlayerPrefs.SetInt("HasSeenIntro", 1);
        PlayerPrefs.Save();

        SkipIntro();
    }

    void SkipIntro()
    {
        videoPlayer.Stop();
        Time.timeScale = 1f;

        // ВКЛЮЧАЕМ UI обратно, когда игра началась
        if (videoUI != null)
            videoUI.SetActive(true);

        if (playerController != null)
            playerController.enabled = true;
    }

    public void ResetIntro()
    {
        PlayerPrefs.DeleteKey("HasSeenIntro");
        PlayerPrefs.Save();
        Debug.Log("Сохранение сброшено!");
    }
}