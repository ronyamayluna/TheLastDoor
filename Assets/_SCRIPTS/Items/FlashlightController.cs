using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private InventoryInv inventory;
    [SerializeField] private string flashlightID = "Flashlight";
    [SerializeField] private GameObject lightObject;

    private string lastItem = null;

    private void Update()
    {
        string currentItem = inventory.GetSelectedItem();

        // если выбор изменился
        if (currentItem != lastItem)
        {
            lastItem = currentItem;

            if (currentItem == flashlightID)
            {
                lightObject.SetActive(true);
            }
            else
            {
                lightObject.SetActive(false);
            }
        }
    }
}
