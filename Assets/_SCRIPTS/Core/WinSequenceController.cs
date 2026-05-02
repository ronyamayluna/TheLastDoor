using System.Collections;
using UnityEngine;

public class WinSequenceController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameLoopFlowController flowController;

    [Header("Settings")]
    [SerializeField] private GameObject winDoor;
    [SerializeField] private GameObject intermediateUI;

    // Вызываем, когда коснулись первого триггера
    public void ShowDoor()
    {
        if (winDoor != null)
        {
            winDoor.SetActive(true);
        }
    }

    // Вызываем, когда коснулись второго триггера
    public void StartWinFlow()
    {
        StartCoroutine(WinRoutine());
    }

    private IEnumerator WinRoutine()
    {
        // 1. Показываем промежуточный экран (катсцену) на 4 секунды
        if (intermediateUI != null)
        {
            intermediateUI.SetActive(true);
            yield return new WaitForSeconds(4f);
            intermediateUI.SetActive(false);
        }

        // 2. Вызываем основной экран победы через контроллер
        if (flowController != null)
        {
            flowController.RequestWinFromExit();
        }
    }
}
