using UnityEngine;

public class SpeakingToy : MonoBehaviour, IInteractable
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip voiceClip;

    // Реализация обязательного метода интерфейса взаимодействия
    public void Interact(PlayerInteraction player)
    {
        // Проверяем, назначены ли аудиокомпоненты
        if (audioSource != null && voiceClip != null)
        {
            // Воспроизводим звук без прерывания текущих звуков
            audioSource.PlayOneShot(voiceClip);
        }
        else
        {
            Debug.LogWarning($"На объекте {gameObject.name} не настроен AudioSource или AudioClip!");
        }
    }
}


