using UnityEngine;

public class PutInBox : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject boxFObject;
    [SerializeField] private GameObject boxSObject;
    [SerializeField] private string requiredItemIDBox = "box2";

    [SerializeField] private GameObject keyObject;
    [SerializeField] private GameObject mirror;
    [SerializeField] private string requiredItemID = "mirror";

    private bool IsSwitched = false;

    // Реализация метода интерфейса взаимодействия
    public void Interact(PlayerInteraction player)
    {
        // Берем инвентарь у игрока, который на нас посмотрел
        InventoryInv inventory = player.Inventory;

        if (inventory == null) return;

        if (!IsSwitched)
        {
            if (inventory.HasItem(requiredItemIDBox))
            {
                inventory.RemoveItem(requiredItemIDBox);
                ReplaceBox();
            }
            else
            {
                Debug.Log("Нужна коробка в инвентаре!");
            }
            return;
        }

        if (IsSwitched)
        {
            if (inventory.HasItem(requiredItemID))
            {
                inventory.RemoveItem(requiredItemID);
                ShowKey();
            }
            else
            {
                Debug.Log("Нужно зеркало!");
            }
        }
    }

    private void ReplaceBox()
    {
        if (boxFObject != null) boxFObject.SetActive(false);
        if (boxSObject != null) boxSObject.SetActive(true);
        IsSwitched = true;
    }

    private void ShowKey()
    {
        if (keyObject != null) keyObject.SetActive(true);
        if (mirror != null) mirror.SetActive(true);
    }
}
