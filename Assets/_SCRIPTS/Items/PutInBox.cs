using UnityEngine;

public class PutInBox : MonoBehaviour
{
    [SerializeField] GameObject boxFObject;
    [SerializeField] GameObject boxSObject;
    [SerializeField] string requiredItemIDBox = "box2";

    [SerializeField] GameObject keyObject;
    [SerializeField] GameObject mirror;
    [SerializeField] string requiredItemID = "mirror";

    private bool IsSwitched = false;

    public void TryPut(InventoryInv inventory)
    {
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