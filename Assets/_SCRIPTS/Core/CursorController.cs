using UnityEngine;

public class CursorControllerc : MonoBehaviour
{
    private void OnEnable()
    {
        if (EventBus.Instance != null)
        {
            EventBus.Instance.OnGamePaused += HandlePaused;
            EventBus.Instance.OnGameResumed += HandleResumed;
        }
    }

    private void OnDisable()
    {
        if (EventBus.Instance != null)
        {
            EventBus.Instance.OnGamePaused -= HandlePaused;
            EventBus.Instance.OnGameResumed -= HandleResumed;
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
