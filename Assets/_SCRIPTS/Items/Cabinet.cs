using UnityEngine;
using UnityEngine.InputSystem;

public class Cabinet : MonoBehaviour
{
    private bool isOpen = false;

    float CabinetOpenPositionZ = 0.4f;
    float CabinetClosePositionZ = 0f;

    [SerializeField] private AudioSource cabinetAudioSource;
    [SerializeField] private AudioClip cabinetOpenSound;
    [SerializeField] private AudioClip cabinetCloseSound;

    public void Update()
    {
        if (isOpen)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, CabinetOpenPositionZ);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, CabinetClosePositionZ);
        }
    }

    public void OpenCabinet()
    {
        isOpen = !isOpen;
        cabinetAudioSource.clip = isOpen ? cabinetOpenSound : cabinetCloseSound;
        cabinetAudioSource.Play();
    }
}
