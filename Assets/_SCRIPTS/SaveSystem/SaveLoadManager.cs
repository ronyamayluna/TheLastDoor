using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;

    private int pendingLoadSlot = -1;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Обязательно отписываемся, чтобы не было утечек памяти
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SaveGame(int slotIndex, Transform player, InventoryInv inventory, TimeScript timer)
    {
        CheckpointSaveData data = new CheckpointSaveData();

        data.checkpointPosition = player.position;
        data.inventoryItems = inventory.GetItems();
        data.selectedSlot = inventory.GetSelectedIndex();
        data.remainingTime = timer.GetRemainingTime();

        CheckpointSaveSystem.Save(slotIndex, data);

        Debug.Log($"Saved slot {slotIndex}");
    }

    public void LoadGame(int slotIndex)
    {
        pendingLoadSlot = slotIndex;

        SceneLoader.Instance.Load(SceneNames.GameScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (pendingLoadSlot == -1)
            return;

        StartCoroutine(LoadRoutine());
    }

    private IEnumerator LoadRoutine()
    {
        yield return null;
        yield return null;

        bool success = CheckpointSaveSystem.TryLoad(pendingLoadSlot, out CheckpointSaveData data);

        if (!success)
        {
            Debug.LogError($"LOAD FAILED: Слот {pendingLoadSlot} не найден или пуст.");
            pendingLoadSlot = -1; // Сбрасываем в любом случае
            yield break;
        }

        PlayerReferences refs = FindFirstObjectByType<PlayerReferences>();

        if (refs == null)
        {
            Debug.LogError("LOAD FAILED: На новой сцене не найден объект с компонентом PlayerReferences!");
            pendingLoadSlot = -1;
            yield break;
        }

        if (refs.Player != null)
        {
            refs.Player.position = data.checkpointPosition;
        }
        if (refs.Inventory != null)
        {
            refs.Inventory.LoadInventory(data.inventoryItems, data.selectedSlot);
        }

        if (refs.Timer != null)
        {
            refs.Timer.SetRemainingTime(data.remainingTime);
        }

        Debug.Log($"LOAD COMPLETE FOR SLOT {pendingLoadSlot}");

        pendingLoadSlot = -1;
    }
}
