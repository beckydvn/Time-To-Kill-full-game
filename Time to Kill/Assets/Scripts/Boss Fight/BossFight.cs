using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BossFight : MonoBehaviour
{
    //holds the combo the user typed in
    private string combo = "";
    //alphabet
    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    //is the player currently attacking? (if not, the boss is)
    private bool attacking = false;
    //get the combo display text
    public TextMeshProUGUI comboDisplay;
    //get the "next move" text
    public TextMeshProUGUI nextMove;
    //get the timer
    public GameObject getTimer;
    //cast
    private BossTimer timer;
    //timer speed
    public float speed;
    //current enemy move
    private string enemyMove;
    //index of the current player move
    private int playerIndex;
    //for enemy
    private int enemyIndex;
    //RNG
    private int rng;
    //number of attacks/defenses combos (all arrays will be the same size)
    int numCombos;

    //boss-specific data
    public string[] playerAttacks;
    public string[] playerDefenses;
    public string[] enemyAttacks;
    public string[] enemyDefenses;

    // Start is called before the first frame update
    void Start()
    {
        timer = (BossTimer)getTimer.GetComponent(typeof(BossTimer));
        timer.setTimer(speed);
        numCombos = playerAttacks.Length;
 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(combo);
        comboDisplay.text = combo;
        if(timer.timeUp())
        {
            NextStage();
        }

        
    }
    private bool checkMove()
    {
        if(attacking)
        {
            playerIndex = Array.IndexOf(playerAttacks, combo);
            enemyIndex = Array.IndexOf(enemyDefenses, enemyMove);
            if(playerIndex == enemyIndex)
            {
                Debug.Log("Critical hit!");
                return true;
            }
        }
        else
        {
            playerIndex = Array.IndexOf(playerDefenses, combo);
            enemyIndex = Array.IndexOf(enemyAttacks, enemyMove);
            if (playerIndex == enemyIndex)
            {
                Debug.Log("Critical defense!");
                return true;
            }
        }
        return false;
    }
    private void NextStage()
    {
        checkMove();
        timer.setTimer(speed);
        attacking = !attacking;
        combo = "";
        if(attacking)
        {
            rng = UnityEngine.Random.Range(0, numCombos);
            enemyMove = enemyDefenses[rng];
        }
        else
        {
            rng = UnityEngine.Random.Range(0, numCombos);
            enemyMove = enemyAttacks[rng];
        }
    }
    private void OnGUI()
    {
        foreach (char letter in alphabet)
        {
            if (Event.current.Equals(Event.KeyboardEvent(letter.ToString())))
            {
                if(combo.Length < 11)
                {
                    combo += letter;
                } 
            }
        }
    }
}
