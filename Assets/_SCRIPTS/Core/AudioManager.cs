using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    
    public AudioSource musicSource;
    public AudioSource pauseMusicSource; 

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        if (EventBus.Instance != null)
        {
            EventBus.Instance.OnGamePaused += HandlePause;
            EventBus.Instance.OnGameResumed += HandleResume;
        }
    }

    private void OnDisable()
    {
        if (EventBus.Instance != null)
        {
            EventBus.Instance.OnGamePaused -= HandlePause;
            EventBus.Instance.OnGameResumed -= HandleResume;
        }
    }

    private void Start()
    {
        if (musicSource != null)
            musicSource.Play();

        if (pauseMusicSource != null)
            pauseMusicSource.Stop();
    }

    private void HandlePause()
    {
        Debug.Log("PAUSE MUSIC");

        if (musicSource != null)
            musicSource.Pause();

        if (pauseMusicSource != null)
            pauseMusicSource.Play();
    }

    private void HandleResume()
    {
        Debug.Log("RESUME MUSIC");

        if (pauseMusicSource != null)
            pauseMusicSource.Stop();

        if (musicSource != null)
            musicSource.UnPause();
    }
}
