using UnityEngine;

public class CursorControllerc : MonoBehaviour
{
    private void OnEnable()
    {
        if (EventBus.Instance != null)
        {
            EventBus.Instance.OnGamePaused += HandlePaused;
            EventBus.Instance.OnGameResumed += HandleResumed;

            // Подписываемся на новое событие конца игры
            EventBus.Instance.OnGameOver += HandlePaused; // Используем тот же метод показа курсора
        }
    }

    private void OnDisable()
    {
        if (EventBus.Instance != null)
        {
            EventBus.Instance.OnGamePaused -= HandlePaused;
            EventBus.Instance.OnGameResumed -= HandleResumed;
            EventBus.Instance.OnGameOver -= HandlePaused;
        }
    }

    private void Start()
    {
        SetGameCursor();
    }

    private void HandlePaused()
    {
        SetMenuCursor();
    }

    private void HandleResumed()
    {
        // Если игра уже проиграна или выиграна, 
        // курсор не должен прятаться обратно при случайных вызовах Resume
        if (GameManager.Instance.CurrentState == GameState.Lost ||
            GameManager.Instance.CurrentState == GameState.Won) return;

        SetGameCursor();
    }

    private void SetGameCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SetMenuCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}