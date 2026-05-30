using UnityEngine;
using System.Collections.Generic;

public class SentencePuzzleManager : MonoBehaviour
{
    public static SentencePuzzleManager Instance;

    [SerializeField] private GameObject puzzleUIPanel;
    [SerializeField] private List<WordSlot> answerSlots;

    [SerializeField] private int[] correctWordIDs;

    [SerializeField] private GameObject keyObject;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckSentence()
    {
        foreach (WordSlot slot in answerSlots)
        {
            if (slot.transform.childCount == 0) return;
        }

        if (IsSentenceCorrect())
        {
            WinPuzzle();
        }
    }

    private bool IsSentenceCorrect()
    {
        for (int i = 0; i < answerSlots.Count; i++)
        {
            DragDropWord word = answerSlots[i].GetComponentInChildren<DragDropWord>();
            if (word == null) return false;

            int wordID;
            if (int.TryParse(word.gameObject.name, out wordID))
            {
                if (wordID != correctWordIDs[i]) return false;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private void WinPuzzle()
    {

        if (puzzleUIPanel != null) puzzleUIPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PlayerController playerController = FindFirstObjectByType<PlayerController>();
        if (playerController != null)
        {
            playerController.EnableMovement();
        }

        InputManager.Instance.SetPauseBlocked(false);

        if (keyObject != null)
        {
            keyObject.SetActive(true);
        }
    }
}
