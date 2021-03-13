using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarryOverInfo : MonoBehaviour
{
    private string journalObjectiveText;
    private string journalPlanetText;
    private float timeLeft = 300;

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void saveObjectiveText(string set)
    {
        journalObjectiveText = set;
    }
    public void savePlanetText(string set)
    {
        journalPlanetText = set;
    }
    public void saveTimeLeft(float set)
    {
        timeLeft = set;
    }
    public float getTimeLeft()
    {
        return timeLeft;
    }
    public string getObjectiveText()
    {
        return journalObjectiveText;
    }

    public string getPlanetText()
    {
        return journalPlanetText;
    }
}
