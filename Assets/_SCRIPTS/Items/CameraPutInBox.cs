using UnityEngine;

public class CameraPutInBox : MonoBehaviour
{
    private float BoxDistance = 3f;
    private InventoryInv inventory;

    private void OnEnable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnInteractPressed += CmPutInBox;
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnInteractPressed -= CmPutInBox;
    }

    public void CmPutInBox()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, BoxDistance))
        {
            PutInBox putInBox = hit.transform.GetComponent<PutInBox>();

            if (putInBox != null && inventory != null)
            {
                putInBox.TryPut(inventory);
            }
        }
    }
}
