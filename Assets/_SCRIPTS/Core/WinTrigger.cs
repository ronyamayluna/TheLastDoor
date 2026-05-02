using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public enum TriggerMode { ActivateDoor, FinalWin }

    [SerializeField] private TriggerMode mode;
    [SerializeField] private WinSequenceController sequenceController;
    [SerializeField] private string requiredTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!IsPlayerCollider(other)) return;

        if (mode == TriggerMode.ActivateDoor)
        {
            sequenceController.ShowDoor();
        }
        else
        {
            sequenceController.StartWinFlow();
        }

        gameObject.SetActive(false); // Деактивируем триггер после касания
    }

    private bool IsPlayerCollider(Collider other)
    {
        if (other == null) return false;
        if (other.CompareTag(requiredTag)) return true;
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag(requiredTag)) return true;

        Transform root = other.transform.root;
        return root != null && root.CompareTag(requiredTag);
    }
}
