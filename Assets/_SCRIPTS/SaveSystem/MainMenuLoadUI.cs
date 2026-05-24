using UnityEngine;
using UnityEngine.UI;

public class MainMenuLoadUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button slot1Button;
    [SerializeField] private Button slot2Button;

    [Header("Panels")]
    [SerializeField] private GameObject loadSlotsPanel;

    private void Start()
    {
        RefreshButtons();

        loadSlotsPanel.SetActive(false);
    }

    public void RefreshButtons()
    {
        slot1Button.interactable = CheckpointSaveSystem.Exists(0);
        slot2Button.interactable = CheckpointSaveSystem.Exists(1);
    }

    public void OpenLoadPanel()
    {
        RefreshButtons();

        loadSlotsPanel.SetActive(true);
    }

    public void CloseLoadPanel()
    {
        loadSlotsPanel.SetActive(false);
    }

    public void LoadSlot1()
    {
        if (!CheckpointSaveSystem.Exists(0))
            return;

        SaveLoadManager.Instance.LoadGame(0);
    }

    public void LoadSlot2()
    {
        if (!CheckpointSaveSystem.Exists(1))
            return;

        SaveLoadManager.Instance.LoadGame(1);
    }
}
