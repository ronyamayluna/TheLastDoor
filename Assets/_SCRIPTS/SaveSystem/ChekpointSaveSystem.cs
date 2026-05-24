using BayatGames.SaveGameFree;
using UnityEngine;

public static class CheckpointSaveSystem
{
    public const int SlotCount = 2;
    private const string SlotKeyPrefix = "checkpoint_progress_";

    public static string GetSlotKey(int slotIndex)
    {
        if (!IsValidSlotIndex(slotIndex))
            return string.Empty;

        return SlotKeyPrefix + slotIndex;
    }
    public static bool Save(int slotIndex, CheckpointSaveData data)
    {
        if (!IsValidSlotIndex(slotIndex))
            return false;

        if (data == null)
        {
            Debug.LogError("CheckpointSaveSystem.Save: data == null.");
            return false;
        }

        string slotKey = GetSlotKey(slotIndex);

        try
        {
            SaveGame.Save(slotKey, data);
            bool jsonExported = CheckpointJsonDebugExporter.Export(slotIndex, data);
            if (!jsonExported)
            {
                Debug.LogWarning(
                    $"CheckpointSaveSystem: �������� ���� Save Game Free '{slotKey}' �������, " +
                    $"�� debug-JSON ��� ����� {slotIndex} �� ������. " +
                    "��������� ����� ������ � Application.persistentDataPath � Console.");
                return false;
            }

            Debug.Log($"CheckpointSaveSystem: ���� {slotIndex} �������� ����� Save Game Free � ������ '{slotKey}'.");
            return true;
        }
        catch (System.Exception exception)
        {
            Debug.LogError($"CheckpointSaveSystem: ������ ���������� ����� {slotIndex}: {exception.Message}");
            return false;
        }
    }
    public static bool Exists(int slotIndex)
    {
        if (!IsValidSlotIndex(slotIndex))
            return false;

        string slotKey = GetSlotKey(slotIndex);

        try
        {
            return SaveGame.Exists(slotKey);
        }
        catch (System.Exception exception)
        {
            Debug.LogError($"CheckpointSaveSystem: ������ �������� ����� {slotIndex}: {exception.Message}");
            return false;
        }
    }
    public static bool TryLoad(int slotIndex, out CheckpointSaveData data)
    {
        data = null;

        if (!IsValidSlotIndex(slotIndex))
            return false;

        if (!Exists(slotIndex))
        {
            return false;
        }

        string slotKey = GetSlotKey(slotIndex);

        try
        {
            data = SaveGame.Load(slotKey, new CheckpointSaveData());
            return data != null;
        }
        catch (System.Exception exception)
        {
            Debug.LogError($"CheckpointSaveSystem: ������ ������ ����� {slotIndex}: {exception.Message}");
            data = null;
            return false;
        }
    }
    private static bool IsValidSlotIndex(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < SlotCount)
            return true;

        Debug.LogError(
            $"CheckpointSaveSystem: �������� ������ ����� {slotIndex}. " +
            $"���������� ��������: 0..{SlotCount - 1}.");
        return false;
    }
}