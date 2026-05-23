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

    [SerializeField] private Transform wheelTransform;
    [SerializeField] private float wheelRotateSpeed = 4f;

    private Quaternion doorOpenRotation;
    private Quaternion wheelTargetRotation;
    private bool isOpened = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Целевой поворот двери: 125 градусов по Y
        doorOpenRotation = Quaternion.Euler(0, 125f, 0);

        // Целевой поворот вентиля: 180 градусов по X
        wheelTargetRotation = Quaternion.Euler(180f, 0, 0);
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
                Debug.Log("Код сброшен.");
                currentInput.Clear();
                return;
            }
        }

        if (currentInput.Count == correctCode.Length)
        {
            Debug.Log("Код верный! Начинаю процесс открытия...");
            isOpened = true;

            StartCoroutine(OpenSequenceRoutine());
        }
    }

    // Последовательная анимация: сначала вентиль, потом дверь
    private IEnumerator OpenSequenceRoutine()
    {
        if (wheelTransform != null)
        {
            while (Quaternion.Angle(wheelTransform.localRotation, wheelTargetRotation) > 0.01f)
            {
                wheelTransform.localRotation = Quaternion.Slerp(
                    wheelTransform.localRotation,
                    wheelTargetRotation,
                    Time.deltaTime * wheelRotateSpeed
                );
                yield return null;
            }
            wheelTransform.localRotation = wheelTargetRotation;
        }

        yield return new WaitForSeconds(0.2f);

        if (doorTransform != null)
        {
            while (Quaternion.Angle(doorTransform.localRotation, doorOpenRotation) > 0.01f)
            {
                doorTransform.localRotation = Quaternion.Slerp(
                    doorTransform.localRotation,
                    doorOpenRotation,
                    Time.deltaTime * openSpeed
                );
                yield return null;
            }
            doorTransform.localRotation = doorOpenRotation;
        }
    }
}


