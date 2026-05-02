using System;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public static EventBus Instance { get; private set; }

    // ������� - ����� ����������� �� ������ �����
    public event Action OnGamePaused;
    public event Action OnGameResumed;

    // В EventBus.cs добавь:
    public event Action OnGameOver;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ������ ��� ������ ������� (raise/trigger)
    public void RaiseGamePaused()
    {
        OnGamePaused?.Invoke(); // �������� �������, ���� ���� ����������
    }

    public void RaiseGameResumed()
    {
        OnGameResumed?.Invoke();
    }

    public void RaiseGameOver()
    {
        OnGameOver?.Invoke();
    }
}
