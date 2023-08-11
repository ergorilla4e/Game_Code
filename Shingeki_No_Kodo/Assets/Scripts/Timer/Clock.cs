using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private float timeRemaining = 0;
    [SerializeField] private bool timeIsRunning = true;
    [SerializeField] private int velocitaTimerApertura = 3;
    [SerializeField] private int velocitaTimerChiusura = 10;

    // Start is called before the first frame update
    void Start()
    {
        timeIsRunning = true;
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    public void SetTimeRemaining(float timeRemaining)
    {
        this.timeRemaining = timeRemaining;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timeIsRunning)
        {
            if(timeRemaining >= 0)
            {
                if (timeRemaining >= 480 && timeRemaining <= 1200)
                {
                    timeRemaining += Time.deltaTime * velocitaTimerApertura;
                }
                else
                {
                    timeRemaining += Time.deltaTime * velocitaTimerChiusura;
                }

                DisplayTime(timeRemaining);

                if(timeRemaining >= 1440)
                {
                    timeRemaining = 0;
                }
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{00:00}:{01:00}",minutes,seconds);
    }

}
