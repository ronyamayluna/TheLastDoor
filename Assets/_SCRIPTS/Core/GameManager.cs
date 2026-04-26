using UnityEngine;

public enum GameState
{
    MainMenu=0,
    Playing=1,
    Paused=2,
    Lost=3,
    Won=4,
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
        Time.timeScale = 0f; // простой вариант паузы
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
    /// Перезапускает игровую сцену через Loading и переводит игру в состояние Playing.
    /// Используется для "New Game" и "Restart" с lose-экрана.
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

    /// <summary>
    /// Переводит игру в состояние поражения и включает UI-ввод.
    /// </summary>
    public void EnterLoseState()
    {
        if (CurrentState != GameState.Playing)
            return;

        CurrentState = GameState.Lost;
        Time.timeScale = 0f;
        if (InputManager.Instance != null)
            InputManager.Instance.EnableUIInput();
        Debug.Log("Game lost");
    }

    /// <summary>
    /// Переводит игру в состояние победы и включает UI-ввод.
    /// </summary>
    public void EnterWinState()
    {
        if (CurrentState != GameState.Playing)
            return;

        CurrentState = GameState.Won;
        Time.timeScale = 0f;
        if (InputManager.Instance != null)
            InputManager.Instance.EnableUIInput();
        Debug.Log("Game won");
    }
}