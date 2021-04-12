using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    private float timeRemaining;
    private Text timeText;
    private bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 60f;
        timeText = GetComponent<Text>();
        timeText.text = formatTime(timeRemaining);
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                isRunning = false;
            }
            timeText.text = formatTime(timeRemaining);
        }
        
    }

    string formatTime(float raw)
    {
        int mins = Mathf.FloorToInt(raw / 60);
        string secs = Mathf.FloorToInt(raw % 60).ToString("D2");
        return "Time " + mins + ":" + secs;
    }
}
