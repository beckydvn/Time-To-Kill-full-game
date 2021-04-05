using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarryOverInfo : MonoBehaviour
{
    private string journalObjectiveText;
    //private string journalPlanetText;
    public float timeLeft;
    //private float timeLeft;
    //array of objects. if the object name is in the array, then the object has already been collected
    //for an npc/object that gives an object, this means the object has already been given
    private List<string> collected = new List<string>();

    private GameObject journal;
    private GameObject inventory;

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        //timeLeft = startTime;
    }
    public void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
    public void NextPlanet(string dest)
    {
        SceneManager.LoadScene("travel");
        StartCoroutine(loadNextPlanet(dest));
    }
    IEnumerator loadNextPlanet(string dest)
    {
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene(dest);
        journal = GameObject.FindGameObjectWithTag("Journal");
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        Destroy(journal);
        Destroy(inventory);
        Destroy(transform.gameObject);
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
}
