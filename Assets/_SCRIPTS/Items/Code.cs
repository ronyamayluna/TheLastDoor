using UnityEngine;

public class Code : MonoBehaviour
{
    [SerializeField] private int myDigit;

    public void PressButtonCode()
    {
        if (CodeSequenceManager.Instance != null)
        {
            CodeSequenceManager.Instance.RegisterButton(myDigit);
        }
    }
}
