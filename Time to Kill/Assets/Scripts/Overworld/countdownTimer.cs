using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdownTimer : MonoBehaviour
{
    public Text timerText;

    private bool isTimer;
    private float timeLeft = 300;

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

    public void setTimeLeft(float set)
    {
        timeLeft = set;
    }

    public float getTimeLeft()
    {
        return timeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameManager.getCollected().ToArray().ToString());
        if (isTimer)
        {
            if(timeLeft > 0)
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
