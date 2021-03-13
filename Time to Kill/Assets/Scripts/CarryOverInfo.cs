using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarryOverInfo : MonoBehaviour
{
    private string journalObjectiveText;
    private string journalPlanetText;

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

    public string getObjectiveText()
    {
        return journalObjectiveText;
    }

    public string getPlanetText()
    {
        return journalPlanetText;
    }
}
