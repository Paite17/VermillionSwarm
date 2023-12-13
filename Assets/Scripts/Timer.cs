using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Time time;
    public TMP_Text timerText;

    private void Start()
    {
        
    }

    private void Update()
    {
        TimeRunning();
    }

    private void TimeRunning()
    {
        DateTime time = DateTime.Now;
        string minute = LeadingZero(time.Minute);
        string second = LeadingZero(time.Second);

        timerText.text = minute + ":" + second;
    }

    string LeadingZero (int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}
