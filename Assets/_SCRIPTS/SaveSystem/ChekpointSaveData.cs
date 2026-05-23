using System;
using UnityEngine;

/// <summary>
/// CheckpointSaveData
/// Что делает: хранит минимальный прогресс для двух checkpoint-сценариев: выход уровня и отдельная safe-point зона.
/// Зачем нужен в игре: checkpoint фиксирует безопасное состояние, а активная волна не попадает в save.
/// Связи: создаётся GameManager, записывается CheckpointSaveSystem, читаться будет flow загрузки.
/// Как используется: trigger выхода или CheckpointTrigger передаёт позицию, затем GameManager сохраняет состояние игрока.
/// Расширения: добавить валюту, открытые двери, список завершённых уровней.
/// Совет: при странном восстановлении проверить completedSceneName, nextSceneName и наличие одного Player.
/// </summary>
[Serializable]
public sealed class CheckpointSaveData
{
    [Header("Версия")]
    [Tooltip("Версия формата save. Нужна, если структура данных изменится в следующих уроках.")]
    public int schemaVersion = 1;

    [Header("Checkpoint")]
    [Tooltip("Читаемый id checkpoint, например LevelComplete_GameScene1.")]
    public string checkpointId;

    [Tooltip("Индекс завершённого уровня в LevelSequenceData. -1 означает fallback-сцену без sequence.")]
    public int completedLevelIndex = -1;

    [Tooltip("Позиция точки сохранения: выход уровня или отдельная safe-point зона.")]
    public Vector3 checkpointPosition;

    [Tooltip("true означает сохранение через выход уровня, false — через отдельный checkpoint-trigger.")]
    public bool savedFromLevelExit;

    [Header("Player")]
    [Tooltip("Инвентарь, слоты на момент сохранения")]
    public float inventory;

}