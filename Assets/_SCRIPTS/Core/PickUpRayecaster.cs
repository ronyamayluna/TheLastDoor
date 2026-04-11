using UnityEngine;

public class CameraPickUp : MonoBehaviour
{
    [SerializeField] float distance = 3f;
    private InventoryInv inventory; 

    private void Start()
    {
        // Ищем обновленный скрипт на игроке или камере
        inventory = GetComponentInParent<InventoryInv>();
    }

    private void OnEnable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnInteractPressed += TryPickUp;
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnInteractPressed -= TryPickUp;
    }

    public void TryPickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            PickableObjects pickable = hit.transform.GetComponent<PickableObjects>();

            if (pickable != null && inventory != null)
            {
                pickable.PickUpObject(inventory);
            }
        }
    }
}
