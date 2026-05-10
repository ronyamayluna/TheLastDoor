using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [Header("UI паузы")]
    [Tooltip("Корневой объект панели паузы.")]
    [SerializeField] private GameObject pausePanel;

    [Tooltip("Кнопка продолжения игры.")]
    [SerializeField] private Button buttonResume;

    [Tooltip("Кнопка возврата в главное меню.")]
    [SerializeField] private Button buttonMainMenu;

    private void Start()
    {
        ValidateReferences();
    }

    private void OnEnable()
    {
        if (EventBus.Instance != null)
        {
            EventBus.Instance.OnGamePaused += ShowPausePanel;
            EventBus.Instance.OnGameResumed += HidePausePanel;
        }

        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnPausePressed += HandlePausePressed;
            InputManager.Instance.OnCancelPressed += HandleCancelPressed;
        }

        if (buttonResume != null)
            buttonResume.onClick.AddListener(OnResumeClicked);

        if (buttonMainMenu != null)
            buttonMainMenu.onClick.AddListener(OnMainMenuClicked);
    }

    private void OnDisable()
    {
        if (EventBus.Instance != null)
        {
            EventBus.Instance.OnGamePaused -= ShowPausePanel;
            EventBus.Instance.OnGameResumed -= HidePausePanel;
        }

        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnPausePressed -= HandlePausePressed;
            InputManager.Instance.OnCancelPressed -= HandleCancelPressed;
        }

        if (buttonResume != null)
            buttonResume.onClick.RemoveListener(OnResumeClicked);

        if (buttonMainMenu != null)
            buttonMainMenu.onClick.RemoveListener(OnMainMenuClicked);
    }

    private void ValidateReferences()
    {
        if (pausePanel == null)
            Debug.LogError($"{name}: pausePanel не назначен.", this);

        if (buttonResume == null)
            Debug.LogWarning($"{name}: buttonResume не назначен.", this);

        if (buttonMainMenu == null)
            Debug.LogWarning($"{name}: buttonMainMenu не назначен.", this);
    }

    private void ShowPausePanel()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    private void HidePausePanel()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    private void OnResumeClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.Resume();
    }

    private void OnMainMenuClicked()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.GoToMenu();
    }

    private void HandlePausePressed()
    {
        if (GameManager.Instance != null && GameManager.Instance.CurrentState == GameState.Playing)
            GameManager.Instance.Pause();
    }

    private void HandleCancelPressed()
    {
        if (GameManager.Instance != null && GameManager.Instance.CurrentState == GameState.Paused)
            GameManager.Instance.Resume();
    }
}