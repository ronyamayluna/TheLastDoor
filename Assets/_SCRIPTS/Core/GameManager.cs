using UnityEngine;

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
}