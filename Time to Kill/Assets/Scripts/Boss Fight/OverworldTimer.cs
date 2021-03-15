using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverworldTimer : MonoBehaviour
{
    public Text timerText;

    private bool isTimer =  true;
    private float timeLeft = 30;
    private GameObject getGameManager;
    private CarryOverInfo gameManager;

    // Start is called before the first frame update
    void Start()
    {
        getGameManager = GameObject.FindGameObjectWithTag("Game Manager");
        gameManager = (CarryOverInfo)getGameManager.GetComponent(typeof(CarryOverInfo));
        timeLeft = gameManager.getTimeLeft();
        isTimer = true;
    }

    void displayTime(float t)
    {
        TimeSpan r = TimeSpan.FromSeconds(t);

        timerText.text = r.ToString("mm':'ss':'ff");
    }
    public void setTimer(float time)
    {
        timeLeft = time;
        isTimer = true;
    }

    public float getTime()
    {
        return timeLeft;
    }

    //check the status of the timer (is time up)
    public bool timeUp()
    {
        return !isTimer;
    }

    public void stopTimer()
    {
        isTimer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimer)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                    displayTime(timeLeft);
            }
            else
            {
                timeLeft = 0;
                isTimer = false;
            }

        }
    }
}

