using UnityEngine;

public class PickableObjects : MonoBehaviour
{
    private bool isPickedUp = false;

    // Теперь метод принимает инвентарь, чтобы добавиться в него
    public void PickUpObject(InventoryInv inventory)
    {
        if (isPickedUp) return;
        isPickedUp = true;

        inventory.AddItem(this.gameObject);
        // Destroy удален, так как предмет теперь "живет" в инвентаре
    }
}