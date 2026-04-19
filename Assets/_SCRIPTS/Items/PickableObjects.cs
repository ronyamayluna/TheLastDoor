// using UnityEngine;

// public class PickableObjects : MonoBehaviour
// {
//     private bool isPickedUp = false;

//     // Теперь метод принимает инвентарь, чтобы добавиться в него
//     public void PickUpObject(InventoryInv inventory)
//     {
//         if (isPickedUp) return;
//         isPickedUp = true;
//         Destroy(this.gameObject);

//     }
// }
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