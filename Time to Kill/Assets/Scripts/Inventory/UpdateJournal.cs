using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateJournal : MonoBehaviour
{
    public TextMeshProUGUI journalObjectiveText; 
    public TextMeshProUGUI journalPlanetText;

    public void setObjectiveText(string set)
    {
        journalObjectiveText.text = set;
    }
    public void setPlanetText(string set)
    {
        journalPlanetText.text = set;
    }
}
