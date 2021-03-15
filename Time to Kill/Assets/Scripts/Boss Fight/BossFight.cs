using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    //public GameObject getTimer;
    //cast
    //private BossTimer timer;
    //timer speed
    public float attackTime;
    public float defenseTime;
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
    //done turn early (only for attacks)
    private bool doneTurn = false;
    //total time taken
    private float time = 0;
    //is the battle still going?
    private bool battleOngoing = true;

    //BASIC ATTACK MOVE
    private string basicAttack = "aaa";
    //BASIC DEFENSE MOVE
    private string basicDefense = "ddd";
    //player wins the boss battle
    private bool win = false;

    //PERCENTAGE OF DAMAGE DONE/TAKEN BY BASIC MOVES (CHANGES EACH BOSS)
    public float percentBasic;
    //percentage for critical damage done/taken
    public float percentCrit;
    //image of player
    public Image player;
    //charged attack bar
    public GameObject getChargedBar;
    private ChargeBar chargedBar;
    //get overworld time
    public GameObject getOverworldTime;
    private OverworldTimer overworldTime;
    //chargebar amount
    private float charge;


    //boss-specific data
    public string[] playerAttacks;
    public string[] playerDefenses;
    public string[] enemyAttacks;
    public string[] enemyDefenses;

    // Start is called before the first frame update
    void Start()
    {
        //timer = (BossTimer)getTimer.GetComponent(typeof(BossTimer));
        playerHealthBar = (HealthBar)getPlayerHealthBar.GetComponent(typeof(HealthBar));
        enemyHealthBar = (HealthBar)getEnemyHealthBar.GetComponent(typeof(HealthBar));
        chargedBar = (ChargeBar)getChargedBar.GetComponent(typeof(ChargeBar));
        overworldTime = (OverworldTimer)getOverworldTime.GetComponent(typeof(OverworldTimer));
        playerHealth = playerHealthBar.getMaxHealth();
        enemyHealth = enemyHealthBar.getMaxHealth();
        charge = chargedBar.getMaxCharge();
        //timer.setTimer(defenseTime);
        numCombos = playerAttacks.Length;
        NextStage();
    }

    // Update is called once per frame
    void Update()
    {
        if (battleOngoing)
        {
            time += Time.deltaTime;
            comboDisplay.text = combo;

            if (!attacking)
            {
                if (!inBuffer)
                {
                    chargedBar.setCharge(chargedBar.getCharge() + 1);
                    if (chargedBar.getCharge() >= charge)
                    {
                        getResults();
                        inBuffer = true;
                        Invoke("NextStage", 1f);
                    }

                }
            }
            //time is up/turn done and the buffer has not already been called
            //if(timer.timeUp() || doneTurnEarly)
            else
            {
                if (!inBuffer && doneTurn)
                {
                    //timer.stopTimer();
                    getResults();
                    inBuffer = true;
                    Invoke("NextStage", 1f);
                }
            }
            if (!inBuffer)
            {
                if(attacking)
                {
                    nextMove.text = bossName + " is preparing to defend himself with " + enemyMove;
                }
                else
                {
                    nextMove.text = bossName + " is preparing to attack you with " + enemyMove;
                }    
            }
        }
        if (overworldTime.timeUp() && battleOngoing)
        {
            GameOver();
        }
    }

    private void getResults()
    {
        if(attacking)
        {
            playerIndex = Array.IndexOf(playerAttacks, combo);
            enemyIndex = Array.IndexOf(enemyDefenses, enemyMove);

            if(playerIndex == enemyIndex)
            {
                enemyHealthBar.setHealth(enemyHealthBar.getHealth() - enemyHealth * percentCrit);
                nextMove.text = "Critical hit!";
            }
            else if(combo == basicAttack)
            {
                nextMove.text = bossName + " Does not seem fazed...";
                enemyHealthBar.setHealth(enemyHealthBar.getHealth() -  enemyHealth * percentBasic);
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
                playerHealthBar.setHealth(playerHealthBar.getHealth() - playerHealth * percentBasic);
            }
            else
            {
                nextMove.text = "Critical damage taken!";
                playerHealthBar.setHealth(playerHealthBar.getHealth() - playerHealth * percentCrit);
            }
        }

        if(enemyHealthBar.getHealth() <= 0)
        {
            GetComponent<Image>().enabled = false;
            battleOngoing = false;
            win = true;
            overworldTime.stopTimer();
            nextMove.text = bossName + " has been defeated! \nTime taken: " + time;
            comboDisplay.text = "VICTORY!";
        }
        if(playerHealthBar.getHealth() <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Destroy(player);
        battleOngoing = false;
        win = false;
        nextMove.text = bossName + " has defeated you!";
        comboDisplay.text = "GAME OVER!";
    }

    //randomnly generate next stage
    private void NextStage()
    {
        attacking = !attacking; 

        chargedBar.setCharge(0);

        combo = "";
        inBuffer = false;
        doneTurn = false;
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
                    combo += letter.ToString().ToLower();
                }
            }
        }
        if (Event.current.Equals(Event.KeyboardEvent("return")))
        {
            //can only end turn early if you are attacking
            if(attacking && battleOngoing)
            {
                doneTurn = true;
            }
            else if(!battleOngoing)
            {
                if(win)
                {
                    //LOAD NEXT PLANET
                    SceneManager.LoadScene("Planet 1");
                }
                else
                {
                    //RESTART CURRENT PLANET
                    SceneManager.LoadScene("Planet 1");
                }
            }
            
        }
    }
}
