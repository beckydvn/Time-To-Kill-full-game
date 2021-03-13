using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSceneLoader : MonoBehaviour
{
    public string nextScene;
    private GameObject getGameManager;
    private CarryOverInfo gameManager;
    private GameObject getTimer;
    private countdownTimer timer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            getGameManager = GameObject.FindGameObjectWithTag("Game Manager");
            gameManager = (CarryOverInfo)getGameManager.GetComponent(typeof(CarryOverInfo));
            getTimer = GameObject.FindGameObjectWithTag("Timer");
            timer = (countdownTimer)getTimer.GetComponent(typeof(countdownTimer));
            gameManager.saveTimeLeft(timer.getTimeLeft());
            SceneManager.LoadScene(nextScene);
            Destroy(gameObject);
        }
        
    }
}
