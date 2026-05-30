using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryInv : MonoBehaviour
{
    [Header("Slots UI")]
    [SerializeField] private Image[] slots;
    [SerializeField] private Sprite emptySprite;

    [Header("Slot Texts")]
    [SerializeField] private TMP_Text[] slotTexts;

    [Header("Selection")]
    [SerializeField] private Color selectedColor = Color.yellow;
    [SerializeField] private Color normalColor = Color.white;

    private string[] items;
    private int selectedIndex = 0;

    public string[] GetItems()
    {
        return items;
    }

    public int GetSelectedIndex()
    {
        return selectedIndex;
    }

    [System.Serializable]
    public class ItemData
    {
        public string id;
        public Sprite icon;
        public string displayName;
    }

    [SerializeField] private ItemData[] itemDatabase;

    private void Start()
    {
        items = new string[slots.Length];
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

    public void LoadInventory(string[] loadedItems, int loadedSelectedIndex)
    {
        items = new string[slots.Length];

        for (int i = 0; i < loadedItems.Length && i < items.Length; i++)
        {
            items[i] = loadedItems[i];
        }

        selectedIndex = Mathf.Clamp(loadedSelectedIndex, 0, slots.Length - 1);

        UpdateUI();
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

            // Подсветка выбранного слота
            slots[i].color = (i == selectedIndex) ? selectedColor : normalColor;

            // Название предмета под выбранным слотом
            if (slotTexts != null && i < slotTexts.Length)
            {
                if (i == selectedIndex && !string.IsNullOrEmpty(items[i]))
                {
                    slotTexts[i].text = GetItemName(items[i]);
                }
                else
                {
                    slotTexts[i].text = "";
                }
            }
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

    private string GetItemName(string id)
    {
        foreach (var item in itemDatabase)
        {
            if (item.id == id)
                return item.displayName;
        }

        return "";
    }

    public void SetItems(string[] loadedItems)
    {
        items = loadedItems;
        UpdateUI();
    }
}