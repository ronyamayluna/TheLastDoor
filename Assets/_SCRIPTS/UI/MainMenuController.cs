using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Кнопки главного меню")]
    [Tooltip("Кнопка начала новой игры.")]
    [SerializeField] private Button buttonNewGame;


    [Tooltip("Кнопка выхода из приложения.")]
    [SerializeField] private Button buttonExit;

    [Header("Окно настроек")]
    [Tooltip("Контроллер отдельной панели настроек в MainMenu.")]
    [SerializeField] private SettingsPanelController settingsPanelController;

    private void Start()
    {
        ValidateReferences();
    }

    private void OnEnable()
    {
        if (buttonNewGame != null)
            buttonNewGame.onClick.AddListener(HandleNewGameClicked);

        if (buttonExit != null)
            buttonExit.onClick.AddListener(HandleExitClicked);
    }

    private void OnDisable()
    {
        if (buttonNewGame != null)
            buttonNewGame.onClick.RemoveListener(HandleNewGameClicked);

        if (buttonExit != null)
            buttonExit.onClick.RemoveListener(HandleExitClicked);

    }

    private void ValidateReferences()
    {
        if (buttonNewGame == null)
            Debug.LogError($"{name}: buttonNewGame не назначен.", this);

        if (buttonExit == null)
            Debug.LogError($"{name}: buttonExit не назначен.", this);


        if (settingsPanelController == null)
            Debug.LogError($"{name}: settingsPanelController не назначен в Inspector.", this);
    }

    private void HandleNewGameClicked()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError($"{name}: GameManager.Instance == null. Нельзя начать игру.", this);
            return;
        }

        GameManager.Instance.StartGame();
    }

    private static void HandleExitClicked()
    {
        Application.Quit();
    }
}
