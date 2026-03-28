using UnityEngine;

public class CameraPickUp : MonoBehaviour
{
    [SerializeField] float distance = 3f;

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractPressed += TryPickUp;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractPressed -= TryPickUp;
        }
    }

    public void TryPickUp()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            PickableObjects pickable = hit.transform.GetComponent<PickableObjects>();

            if (pickable != null)
            {
                Debug.Log("Object picked up");
                pickable.PickUpObject();
            }
        }
    }
}
