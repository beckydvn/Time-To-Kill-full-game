using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void pressPlay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void quit()
    {
        Debug.Log("Quit");
    }
}