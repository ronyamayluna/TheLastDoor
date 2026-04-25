using UnityEngine;

public class PickableObjects : MonoBehaviour
{
    [SerializeField] string itemID;
    private bool isPickedUp = false;

    public void PickUpObject(InventoryInv inventory)
    {
        if (isPickedUp) return;

        isPickedUp = true;

        inventory.AddItem(itemID);

        Destroy(gameObject);
    }
}