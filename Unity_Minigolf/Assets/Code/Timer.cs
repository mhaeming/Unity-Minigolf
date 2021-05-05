using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float _timeLimit = 60;
    private float _remainingTime;
    public Text timerText;

    private void Start()
    {
        Debug.Log(String.Format("Set time limit to {0}", _timeLimit));
    }

    void Update()
    {
        _remainingTime = _timeLimit - Time.timeSinceLevelLoad;
        DisplayTime(_remainingTime);
        // start displaying the remaining time in red when there are only 10 secs left
        if (_remainingTime <= 11)
        {
            DisplayTimeInRed(_remainingTime);
        }
        // don't display any negative values
        if (_remainingTime <= 1)
        {
            DisplayTimeInRed(0);
        }
    }
    
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void DisplayTimeInRed(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.color = Color.red;
    }
    
}
