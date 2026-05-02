using System;
using TMPro;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI[] timerText;

    [Header("Timer Settings")]
    [SerializeField] private float remainingTime;

    // Событие, на которое подписывается TimerLoseSequence
    public event Action OnTimerEnd;

    private bool timerIsRunning = true;

    void Update()
    {
        // Если таймер остановлен (победа или телепорт), выходим
        if (!timerIsRunning) return;

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateDisplay();
        }
        else
        {
            // Таймер дошел до нуля
            remainingTime = 0;
            UpdateDisplay();

            timerIsRunning = false; // Выключаем таймер
            OnTimerEnd?.Invoke();   // Запускаем последовательность проигрыша
        }
    }

    // Метод для обновления текста на всех назначенных объектах TMP
    private void UpdateDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

        foreach (var text in timerText)
        {
            if (text != null)
            {
                text.text = timeString;
            }
        }
    }

    /// <summary>
    /// Вызывай этот метод из скрипта телепорта или контроллера победы,
    /// чтобы остановить отсчет и предотвратить плохую концовку.
    /// </summary>
    public void StopTimer()
    {
        timerIsRunning = false;
        Debug.Log("<color=green>Таймер остановлен успешно!</color>");
    }

    /// <summary>
    /// Если понадобится добавить время (бонус), можно использовать этот метод.
    /// </summary>
    public void AddTime(float amount)
    {
        remainingTime += amount;
    }
}