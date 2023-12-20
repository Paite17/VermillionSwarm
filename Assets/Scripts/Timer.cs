using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timer;
    private int currentMinute;
    private int currentSecond;
    public TMP_Text timerText;

    private void Start()
    {
        // Initialize timer to 0
        timer = 0f;
    }

    private void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Calculate minutes and seconds
        currentMinute = Mathf.FloorToInt(timer / 60f);
        currentSecond = Mathf.FloorToInt(timer % 60f);

        TimeRunning();
    }
    private void TimeRunning()
    {
        timerText.text = "Time - " + $"{currentMinute:00}" + ":" + $"{currentSecond:00}";
    }
}
