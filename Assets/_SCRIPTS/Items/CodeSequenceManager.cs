using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CodeSequenceManager : MonoBehaviour
{
    public static CodeSequenceManager Instance;

    [SerializeField] private int[] correctCode = { 7, 5, 5, 4 };
    private List<int> currentInput = new List<int>();

    [SerializeField] private Transform doorTransform;
    [SerializeField] private float openSpeed = 2f;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    private bool isOpened = false;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterButton(int digit)
    {
        if (isOpened) return;

        currentInput.Add(digit);
        Debug.Log($"Введена цифра: {digit}. Текущая длина: {currentInput.Count}");

        CheckCode();
    }

    private void CheckCode()
    {
        for (int i = 0; i < currentInput.Count; i++)
        {
            if (currentInput[i] != correctCode[i])
            {
                Debug.Log("<color=red>Ошибка!</color> Код сброшен.");
                currentInput.Clear();
                return;
            }
        }

        // Если дошли сюда и длины совпали — код верный
        if (currentInput.Count == correctCode.Length)
        {
            Debug.Log("<color=green>Успех!</color> Открываю сейф...");
            isOpened = true;
        }
    }

    //private IEnumerator OpenDoorRoutine()
    //{
    //    float elapsed = 0;
    //    while (Quaternion.Angle(doorTransform.localRotation, openRotation) > 0.01f)
    //    {
    //        doorTransform.localRotation = Quaternion.Slerp(
    //            doorTransform.localRotation,
    //            openRotation,
    //            Time.deltaTime * openSpeed
    //        );
    //        yield return null;
    //    }
    //    doorTransform.localRotation = openRotation;
    //}
}
