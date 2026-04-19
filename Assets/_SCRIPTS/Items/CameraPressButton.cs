using UnityEngine;

public class CameraPressButton : MonoBehaviour
{
    [SerializeField] private float interactDistance = 5f;

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
            ButtonThirdRoom button = hit.transform.GetComponent<ButtonThirdRoom>();

            if (button != null)
            {
                button.PressButton();
            }
        }
    }
}
