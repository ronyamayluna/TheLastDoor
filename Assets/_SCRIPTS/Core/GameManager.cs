using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu = 0,
    Playing = 1,
    Paused = 2,
    Lost = 3,
    Won = 4,

    
}

public class GameManager : MonoBehaviour
{
    private int activeSaveSlotIndex = 0; // Индекс текущего слота сохранения
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; } = GameState.MainMenu;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        RestartGameScene();
    }

    public void GoToMenu()
    {
        CurrentState = GameState.MainMenu;
        Time.timeScale = 1f;
        SceneLoader.Instance.Load(SceneNames.MainMenu);
        if (InputManager.Instance != null)
            InputManager.Instance.EnableUIInput();
        Debug.Log("Go to Main Menu");
    }

    public void Pause()
    {
        if (CurrentState != GameState.Playing)
            return;

        CurrentState = GameState.Paused;
        Time.timeScale = 0f; // ������� ������� �����
        EventBus.Instance.RaiseGamePaused();
        Debug.Log("Game paused");
    }

    public void Resume()
    {
        if (CurrentState != GameState.Paused)
            return;

        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
        EventBus.Instance.RaiseGameResumed();
        Debug.Log("Game resumed");
    }

    /// <summary>
    /// ������������� ������� ����� ����� Loading � ��������� ���� � ��������� Playing.
    /// ������������ ��� "New Game" � "Restart" � lose-������.
    /// </summary>
    public void RestartGameScene()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
        SceneLoader.Instance.LoadWithLoading(SceneNames.GameScene);
        Debug.Log("Game scene restart requested");
        if (InputManager.Instance != null)
            InputManager.Instance.EnablePlayerInput();
    }



    public void EnterLoseState()
    {
        if (CurrentState != GameState.Playing) return;

        CurrentState = GameState.Lost;
        Time.timeScale = 0f;

        EventBus.Instance.RaiseGameOver();

        if (InputManager.Instance != null)
            InputManager.Instance.EnableUIInput();

    }

    public void EnterWinState()
    {
        if (CurrentState != GameState.Playing) return;

        CurrentState = GameState.Won;
        Time.timeScale = 0f;

        // Оповещаем мир, что игра окончена
        EventBus.Instance.RaiseGameOver();

        if (InputManager.Instance != null)
            InputManager.Instance.EnableUIInput();

    }

    public bool TrySaveCheckpointProgress(Vector3 checkpointPosition)
    {
        if (!IsValidSaveSlot(activeSaveSlotIndex))
        {
            Debug.LogError("GameManager: save slot is not selected. Checkpoint was not saved.", this);
            return false;
        }

        if (!TryBuildCheckpointData(
                $"Checkpoint_{SceneManager.GetActiveScene().name}",
                checkpointPosition,
                false,
                out CheckpointSaveData data))
            return false;

        return TrySaveCheckpointData(activeSaveSlotIndex, data);
    }
    private bool IsValidSaveSlot(int slotIndex)
    {
        return slotIndex >= 0 && slotIndex < CheckpointSaveSystem.SlotCount;
    }

    private bool TryBuildCheckpointData(string id, Vector3 position, bool savedFromLevelExit, out CheckpointSaveData data)
    {
        data = new CheckpointSaveData
        {
            checkpointId = id,
            checkpointPosition = position,
            savedFromLevelExit = savedFromLevelExit
        };
        return true;
    }

    private bool TrySaveCheckpointData(int slotIndex, CheckpointSaveData data)
    {
        return CheckpointSaveSystem.Save(slotIndex, data);
    }
}