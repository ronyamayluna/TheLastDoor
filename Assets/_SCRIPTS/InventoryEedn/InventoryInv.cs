using UnityEngine;
using UnityEngine.UI;

public class InventoryInv : MonoBehaviour
{
    [Header("Slots UI")]
    [SerializeField] private Image[] slots;
    [SerializeField] private Sprite emptySprite;

    [Header("Selection")]
    [SerializeField] private Color selectedColor = Color.yellow;
    [SerializeField] private Color normalColor = Color.white;

    private string[] items; // ВАЖНО: фиксированные слоты
    private int selectedIndex = 0;

    [System.Serializable]
    public class ItemData
    {
        public string id;
        public Sprite icon;
    }

    [SerializeField] private ItemData[] itemDatabase;

    private void Start()
    {
        items = new string[slots.Length]; // фиксируем размер
        UpdateUI();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedIndex = i;
                UpdateUI();
            }
        }
    }

    // 🔥 ДОБАВЛЕНИЕ В ПЕРВЫЙ СВОБОДНЫЙ СЛОТ
    public bool AddItem(string itemID)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (string.IsNullOrEmpty(items[i]))
            {
                items[i] = itemID;
                UpdateUI();
                return true;
            }
        }

        Debug.Log("Инвентарь полон!");
        return false;
    }

    public void RemoveItem(string itemID)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemID)
            {
                items[i] = null;
                UpdateUI();
                return;
            }
        }
    }

    public string GetSelectedItem()
    {
        if (selectedIndex >= 0 && selectedIndex < items.Length)
            return items[selectedIndex];

        return null;
    }

    public bool HasItem(string itemID)
    {
        foreach (var item in items)
        {
            if (item == itemID)
                return true;
        }
        return false;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!string.IsNullOrEmpty(items[i]))
            {
                slots[i].sprite = GetIcon(items[i]);
                slots[i].enabled = true;
            }
            else
            {
                slots[i].sprite = emptySprite;
            }

            slots[i].color = (i == selectedIndex) ? selectedColor : normalColor;
        }
    }

    private Sprite GetIcon(string id)
    {
        foreach (var item in itemDatabase)
        {
            if (item.id == id)
                return item.icon;
        }
        return null;
    }
}