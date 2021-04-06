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
    private GameObject equipCanvas;

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        //timeLeft = startTime;
    }
    public void GameOver()
    {
        StartCoroutine(GameOverPause());
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
        Reset();
    }
    IEnumerator GameOverPause()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Game Over");
        Reset();
    }
    private void Reset()
    {
        journal = GameObject.FindGameObjectWithTag("Journal");
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        equipCanvas = GameObject.FindGameObjectWithTag("Equip Canvas");
        Destroy(journal);
        Destroy(inventory);
        Destroy(equipCanvas);
        for(int i = 0; i < collected.Count; i++)
        {
            GameObject obj = GameObject.FindGameObjectWithTag(collected[i]);
            Destroy(obj);
        }
        GameObject[] objs = GameObject.FindGameObjectsWithTag("UI Inventory Object");
        for (int i = 0; i < objs.Length; i++)
        {
            Destroy(objs[i]);
        }
            
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
