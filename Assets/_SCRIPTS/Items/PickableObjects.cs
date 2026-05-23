using UnityEngine;

// Подключаем наш интерфейс взаимодействия
public class PickableObjects : MonoBehaviour, IInteractable
{
    [SerializeField] private string itemID;
    private bool isPickedUp = false;

    // Реализация метода интерфейса
    public void Interact(PlayerInteraction player)
    {
        if (isPickedUp) return;

        // Берем инвентарь у игрока, который на нас посмотрел
        InventoryInv inventory = player.Inventory;

        if (inventory != null)
        {
            // Пытаемся добавить предмет в инвентарь
            bool success = inventory.AddItem(itemID);

            // Если в инвентаре было свободное место — подбираем
            if (success)
            {
                isPickedUp = true;
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Инвентарь забит! Не удается подобрать: " + itemID);
            }
        }
    }
}
