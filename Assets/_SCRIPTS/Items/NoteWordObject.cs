using UnityEngine;

public class NoteWorldObject : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject puzzleUIPanel;

    public void Interact(PlayerInteraction player)
    {
        if (puzzleUIPanel != null)
        {
            puzzleUIPanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            PlayerController controller = player.GetComponentInParent<PlayerController>();
            if (controller != null)
            {
                controller.DisableMovement();
            }

            InputManager.Instance.SetPauseBlocked(true);
        }
    }
}


