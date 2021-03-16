using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarryOverInfo : MonoBehaviour
{
    private string journalObjectiveText;
    //private string journalPlanetText;
    private float timeLeft = 300;
    //array of objects. if the object name is in the array, then the object has already been collected
    //for an npc/object that gives an object, this means the object has already been given
    private List<string> collected = new List<string>();
 

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    public void newItemCollected(string set)
    {
        collected.Add(set);
    }
    public bool itemCollected(string item)
    {
        return collected.Contains(item);
    }
    public List<string> getCollected()
    {
        return collected;
    }
    public void saveObjectiveText(string set)
    {
        journalObjectiveText = set;
    }
    //public void savePlanetText(string set)
    //{
    //    journalPlanetText = set;
    //}
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
    //public string getPlanetText()
    //{
    //    return journalPlanetText;
    //}
}
