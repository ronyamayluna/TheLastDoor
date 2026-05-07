using UnityEngine;

public class CameraCodePress : MonoBehaviour
{
    [SerializeField] private float interactDistance = 5f;
    [SerializeField] private LayerMask buttonLayer;

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractPressed += PressButtonRaycast;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractPressed -= PressButtonRaycast;
        }
    }

    private void PressButtonRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
        {
            Code button = hit.transform.GetComponent<Code>();

            if (button != null)
            {
                button.PressButtonCode();
            }
        }
    }
}
