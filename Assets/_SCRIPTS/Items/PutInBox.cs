using UnityEngine;

public class PutInBox : MonoBehaviour
{
    [SerializeField] GameObject keyObject;
    [SerializeField] GameObject mirror;
    [SerializeField] string requiredItemID = "mirror"; // ID предмета, который нужно положить в коробку

    public void TryPut(InventoryInv inventory)
    {

        if (string.IsNullOrEmpty(requiredItemID))
        {
            ShowKey();
            return;
        }

        if (inventory.HasItem(requiredItemID))
        {
            inventory.RemoveItem(requiredItemID);
            ShowKey();
        }
        
    }

    private void ShowKey()
    {
        if (keyObject != null)
        {
            keyObject.SetActive(true);
        }
        if (mirror != null)
        {
            mirror.SetActive(true);
        }
    }
}

