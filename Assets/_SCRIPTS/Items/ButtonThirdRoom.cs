using UnityEngine;
using System.Collections;
public class ButtonThirdRoom : MonoBehaviour, IInteractable
{
    private bool isPressed = false;

    [Header("Settings")]
    [SerializeField] private float pressedPosY = -0.117f;
    [SerializeField] private float normalPosY = 0f;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float resetTime = 3f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pressSound;

    [Header("ID")]
    public int buttonID; // для последовательности

    private Coroutine pressRoutine;

    public void Interact(PlayerInteraction player)
    {
        PressButton();
    }

    private void Update()
    {
        float targetY = isPressed ? pressedPosY : normalPosY;

        Vector3 targetPos = new Vector3(
            transform.localPosition.x,
            targetY,
            transform.localPosition.z
        );

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPos,
            Time.deltaTime * smoothSpeed
        );
    }

    public void PressButton()
    {
        if (pressRoutine != null) return;

        pressRoutine = StartCoroutine(PressRoutine());

        if (audioSource != null && pressSound != null)
        {
            audioSource.PlayOneShot(pressSound);
        }

        // уведомляем менеджер последовательности
        if (ButtonSequenceManager.Instance != null)
        {
            ButtonSequenceManager.Instance.RegisterButton(buttonID);
        }
    }

    private IEnumerator PressRoutine()
    {
        isPressed = true;

        yield return new WaitForSeconds(resetTime);

        isPressed = false;
        pressRoutine = null;
    }
}