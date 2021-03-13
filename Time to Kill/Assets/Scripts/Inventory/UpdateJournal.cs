using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateJournal : MonoBehaviour
{
    public TextMeshProUGUI journalObjectiveText; 
    public TextMeshProUGUI journalPlanetText;
    private GameObject getGameManager;
    private CarryOverInfo gameManager;

    private void Start()
    {
        getGameManager = GameObject.FindGameObjectWithTag("Game Manager");
        gameManager = (CarryOverInfo)getGameManager.GetComponent(typeof(CarryOverInfo));
        setObjectiveText(gameManager.getObjectiveText());
        setPlanetText(gameManager.getPlanetText());
        //DontDestroyOnLoad(transform.gameObject);
    }
    private void Update()
    {
        if(transform.gameObject.activeInHierarchy)
        {

        }

    }

    public void setObjectiveText(string set)
    {
        gameManager.saveObjectiveText(set);
        journalObjectiveText.text = set;
    }
    public void setPlanetText(string set)
    {
        gameManager.savePlanetText(set);
        journalPlanetText.text = set;
    }
}
