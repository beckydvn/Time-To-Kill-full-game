using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void pressPlay()
    {
        SceneManager.LoadScene("Player Interior");
    }

    public void pressControls()
    {
        SceneManager.LoadScene("controls");
    }

    public void pressBack()
    {
        SceneManager.LoadScene("Menu Start");
    }

    public void quit()
    {
        Debug.Log("Quit");
    }
}
