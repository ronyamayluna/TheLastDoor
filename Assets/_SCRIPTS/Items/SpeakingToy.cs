using UnityEngine;

public class SpeakingToy : MonoBehaviour, IInteractable
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip voiceClip;

    private bool isPlaying = false;

    public void Interact(PlayerInteraction player)
    {
        // Если звук уже играет — повторно нажать нельзя
        if (isPlaying)
            return;

        if (audioSource != null && voiceClip != null)
        {
            audioSource.PlayOneShot(voiceClip);

            // Запоминаем, что сейчас идёт воспроизведение
            isPlaying = true;

            // Через длину клипа снова разрешаем взаимодействие
            Invoke(nameof(ResetInteraction), voiceClip.length);
        }
        else
        {
            Debug.LogWarning($"На объекте {gameObject.name} не настроен AudioSource или AudioClip!");
        }
    }

    private void ResetInteraction()
    {
        isPlaying = false;
    }
}

