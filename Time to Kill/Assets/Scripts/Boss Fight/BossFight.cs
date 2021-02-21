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
    //name of boss
    public string bossName;
    //is the player in a buffer stage?
    private bool inBuffer = false;
    //player health bar
    public GameObject getPlayerHealthBar;
    private HealthBar playerHealthBar;
    //enemy health bar
    public GameObject getEnemyHealthBar;
    private HealthBar enemyHealthBar;
    float playerHealth;
    float enemyHealth;

    //BASIC ATTACK MOVE
    private string basicAttack = "AAA";
    //BASIC DEFENSE MOVE
    private string basicDefense = "DDD";

    //boss-specific data
    public string[] playerAttacks;
    public string[] playerDefenses;
    public string[] enemyAttacks;
    public string[] enemyDefenses;

    //PERCENTAGE OF DAMAGE DONE/TAKEN BY BASIC MOVES (CHANGES EACH BOSS)
    public float percentBasic;

    private bool doneTurnEarly = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = (BossTimer)getTimer.GetComponent(typeof(BossTimer));
        playerHealthBar = (HealthBar)getPlayerHealthBar.GetComponent(typeof(HealthBar));
        enemyHealthBar = (HealthBar)getEnemyHealthBar.GetComponent(typeof(HealthBar));
        playerHealth = playerHealthBar.getMaxHealth();
        enemyHealth = enemyHealthBar.getMaxHealth();
        timer.setTimer(speed);
        numCombos = playerAttacks.Length;
        NextStage();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer.timeUp());
        comboDisplay.text = combo;
        //time is up and the buffer has not already been called
        if(timer.timeUp() || doneTurnEarly)
        {
            if(!inBuffer)
            {
                timer.stopTimer();
                getResults();
                inBuffer = true;   
                Invoke("NextStage", 2.5f);
            }
        }
        else
        {
            nextMove.text = bossName + " is preparing " + enemyMove;
        }
    }

    private void getResults()
    {
        if(attacking)
        {
            playerIndex = Array.IndexOf(playerAttacks, combo);
            enemyIndex = Array.IndexOf(enemyDefenses, enemyMove);
            
            //deal extra damage based on a time bonus?

            if(playerIndex == enemyIndex)
            {
                enemyHealthBar.setHealth(enemyHealthBar.getHealth() - enemyHealth * 0.3f);
                nextMove.text = "Critical hit!";
            }
            else if(combo == basicAttack)
            {
                nextMove.text = bossName + " Does not seem fazed...";
                enemyHealthBar.setHealth(enemyHealthBar.getHealth() -  enemyHealth * 0.1f);
            }
            else
            {
   
                nextMove.text = bossName + " Admires the lovely weather.";
            }
        }
        else
        {
            playerIndex = Array.IndexOf(playerDefenses, combo);
            enemyIndex = Array.IndexOf(enemyAttacks, enemyMove);
            
            
            if (playerIndex == enemyIndex)
            {
                nextMove.text = bossName + " missed!";
            }
            else if (combo == basicDefense)
            {
                nextMove.text = "You start to drip sweat...";
                playerHealthBar.setHealth(playerHealthBar.getHealth() - playerHealth * 0.1f);
            }
            else
            {
                nextMove.text = "Critical damage taken!";
                playerHealthBar.setHealth(playerHealthBar.getHealth() - playerHealth * 0.3f);
            }
        }
    }

    //randomnly generate next stage
    private void NextStage()
    {
        timer.setTimer(speed);
        attacking = !attacking;
        combo = "";
        inBuffer = false;
        doneTurnEarly = false;
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
        if (Event.current.Equals(Event.KeyboardEvent("return")))
        {
            //can only end turn early if you are attacking
            if(attacking)
            {
                doneTurnEarly = true;
            }
            
        }
    }
}
