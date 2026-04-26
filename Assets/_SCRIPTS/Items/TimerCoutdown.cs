using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] timerText;
    [SerializeField] float remainingTime;
    
    public System.Action OnTimerEnd;

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            remainingTime = 0;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        foreach (var text in timerText)
        {
            if (text != null)
            {
                text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
    }
}
