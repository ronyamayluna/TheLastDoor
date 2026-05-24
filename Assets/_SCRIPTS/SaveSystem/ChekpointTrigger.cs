using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CheckpointTrigger : MonoBehaviour
{
    [Header("Save Slot")]
    [SerializeField] private int slotIndex;

    [Header("References")]
    [SerializeField] private InventoryInv inventory;
    [SerializeField] private TimeScript timer;

    [Header("Debug")]
    [SerializeField] private bool oneTimeUse = true;

    private bool wasUsed;

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (wasUsed && oneTimeUse)
            return;

        if (!other.CompareTag("Player"))
            return;

        SaveLoadManager.Instance.SaveGame(
            slotIndex,
            other.transform,
            inventory,
            timer);

        wasUsed = true;

        Debug.Log($"Checkpoint saved in slot {slotIndex}");
    }
}
