using UnityEngine;
using System.Collections.Generic;

public class ButtonSequenceManager : MonoBehaviour
{
    public static ButtonSequenceManager Instance;

    [Header("Sequence")]
    [SerializeField] private List<int> correctSequence = new List<int>();

    private List<int> currentSequence = new List<int>();

    [Header("Reward")]
    [SerializeField] private GameObject keyObject;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterButton(int id)
    {
        currentSequence.Add(id);

        CheckSequence();
    }

    private void CheckSequence()
    {
        // проверяем по мере ввода
        for (int i = 0; i < currentSequence.Count; i++)
        {
            if (currentSequence[i] != correctSequence[i])
            {
                ResetSequence();
                return;
            }
        }

        // если последовательность полностью совпала
        if (currentSequence.Count == correctSequence.Count)
        {
            ActivateKey();
            ResetSequence();
        }
    }

    private void ResetSequence()
    {
        currentSequence.Clear();
    }

    private void ActivateKey()
    {
        if (keyObject != null)
        {
            keyObject.SetActive(true);
        }

        
    }
}
