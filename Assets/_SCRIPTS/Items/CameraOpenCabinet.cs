using Unity.VisualScripting;
using UnityEngine;

public class CameraOpenCabinet : MonoBehaviour
{
    [SerializeField] float openCabinetDistance = 5f;

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractPressed += OpenCabinetRayecast;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractPressed -= OpenCabinetRayecast;
        }
    }

    public void OpenCabinetRayecast()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, openCabinetDistance))
        {
            Cabinet cabinet = hit.transform.GetComponent<Cabinet>();
            if (cabinet != null)
            {
                cabinet.OpenCabinet();
            }
        }
    }

}
