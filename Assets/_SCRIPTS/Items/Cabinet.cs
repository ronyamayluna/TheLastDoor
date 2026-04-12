using UnityEngine;
using UnityEngine.InputSystem;

public class Cabinet : MonoBehaviour
{
    private bool isOpen = false;

    [Header("Settings")]
    [SerializeField] private float CabinetOpenPositionZ = 0.4f;
    [SerializeField] private float CabinetClosePositionZ = 0f;
    [SerializeField] private float smoothSpeed = 5.0f;

    [Header("Audio")]
    [SerializeField] private AudioSource cabinetAudioSource;
    [SerializeField] private AudioClip cabinetOpenSound;
    [SerializeField] private AudioClip cabinetCloseSound;

    public void Update()
    {
        float targetZ = isOpen ? CabinetOpenPositionZ : CabinetClosePositionZ;
        Vector3 targetPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, targetZ);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothSpeed);
    }

    public void OpenCabinet()
    {
        isOpen = !isOpen;

        if (cabinetAudioSource != null)
        {
            cabinetAudioSource.clip = isOpen ? cabinetOpenSound : cabinetCloseSound;
            cabinetAudioSource.Play();
        }
    }
}