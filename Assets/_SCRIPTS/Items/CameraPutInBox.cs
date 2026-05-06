using UnityEngine;

public class CameraPutInBox : MonoBehaviour
{
    [SerializeField] private float boxDistance = 3f;

    private InventoryInv inventory;

    private void Awake()
    {
        inventory = FindFirstObjectByType<InventoryInv>();
    }

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractPressed += CmPutInBox;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractPressed -= CmPutInBox;
        }
    }

    public void CmPutInBox()
    {
        if (inventory == null) return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, boxDistance))
        {
            PutInBox putInBox = hit.transform.GetComponent<PutInBox>();

            if (putInBox != null)
            {
                putInBox.TryPut(inventory);
            }
        }
    }
}