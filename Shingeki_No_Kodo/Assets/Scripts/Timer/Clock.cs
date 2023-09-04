using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Clock : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private float timeRemaining = 0;
    [SerializeField] private bool timeIsRunning = true;
    [SerializeField] private int velocitaTimerApertura = 3;
    [SerializeField] private int velocitaTimerChiusura = 10;
    [SerializeField] private TMP_Text Day;

    private int contatoreGiorni = 1;
    private int tempoDiApertura = 480; //8:00 di mattina
    private int tempoDiChiusura = 1200; //20:00 di sera

    private void Start()
    {
        timeIsRunning = true;
        contatoreGiorni = 1;
        Day.text = "" + contatoreGiorni;
    }

    public int GetTempoDiApertura()
    {
        return tempoDiApertura;
    }

    public int GetTempoDiChiusura()
    {
        return tempoDiChiusura;
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    public void SetTimeRemaining(float timeRemaining)
    {
        this.timeRemaining = timeRemaining;
    }

    public bool GetTimeIsRunning()
    {
        return timeIsRunning;
    }

    public void SetTimeIsRunning(bool timeIsRunning)
    {
        this.timeIsRunning = timeIsRunning;
    }

    private void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemaining >= 0)
            {
                if (timeRemaining >= tempoDiApertura && timeRemaining <= tempoDiChiusura)
                {
                    timeRemaining += Time.deltaTime * velocitaTimerApertura;
                }
                else
                {
                    timeRemaining += Time.deltaTime * velocitaTimerChiusura;
                }

                DisplayTime(timeRemaining);

                if (timeRemaining >= 1440)
                {
                    timeRemaining = 0;
                    contatoreGiorni++;
                    Day.text = "" + contatoreGiorni;

                    if (contatoreGiorni > 5)
                    {
                        timeIsRunning = false;
                        goToEndGameScene();
                    }
                }
            }
        }
    }

    public void goToEndGameScene()
    {
        PassaggioScene.Instance.StartFadeToOpaque(
        () =>
        {
            SceneManager.LoadScene(2);
            PassaggioScene.Instance.StartFadeToTransparent();
        });
    }

    public void goToLoseGameScene()
    {
        PassaggioScene.Instance.StartFadeToOpaque(
        () =>
        {
            SceneManager.LoadScene(3);
            PassaggioScene.Instance.StartFadeToTransparent();
        });
    }

    //Funzione che formatta la visualizzazione del tempo in sessantesimi per la visualizzazione sulla UI
    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); //Restituisce le ore
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); //Restituisce i minuti 
        timeText.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }

    public int GetContatoreGiorni()
    {
        return contatoreGiorni;
    }

}
