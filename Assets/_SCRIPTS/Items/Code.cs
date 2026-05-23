using UnityEngine;

public class Code : MonoBehaviour, IInteractable
{
    [SerializeField] private int myDigit;

    public void Interact(PlayerInteraction player)
    {
        PressButtonCode();
    }

    public void PressButtonCode()
    {
        if (CodeSequenceManager.Instance != null)
        {
            CodeSequenceManager.Instance.RegisterButton(myDigit);
        }
    }
}

