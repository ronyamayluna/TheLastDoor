using UnityEngine;
using System.Collections.Generic;

public class InventoryInv : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
    public Transform hand; 
    private int currentIndex = -1;

    void Update()
    {
        // Проверка нажатия цифр 1-9
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectItem(i);
            }
        }
    }

    public void AddItem(GameObject item)
    {
        slots.Add(item);
        item.SetActive(false); // Скрываем после подбора

        // Устанавливаем предмет в "руку", но оставляем выключенным
        if (hand != null)
        {
            item.transform.SetParent(hand);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
        }

        // Если это первый предмет, выбираем его сразу
        if (slots.Count == 1) SelectItem(0);
    }

    void SelectItem(int index)
    {
        if (index >= slots.Count) return;

        // Выключаем текущий предмет
        if (currentIndex != -1 && currentIndex < slots.Count)
            slots[currentIndex].SetActive(false);

        currentIndex = index;

        // Включаем новый выбранный предмет
        slots[currentIndex].SetActive(true);
        Debug.Log("Выбран предмет: " + slots[currentIndex].name);
    }
}
