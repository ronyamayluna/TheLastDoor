using System;
using UnityEngine;

[Serializable]
public sealed class CheckpointSaveData
{
    [Header("Checkpoint")]
    public string checkpointId;
    public Vector3 checkpointPosition;
    public bool savedFromLevelExit;

    [Header("Inventory")]
    public string[] inventoryItems;
    public int selectedSlot;

    [Header("Timer")]
    public float remainingTime;
}