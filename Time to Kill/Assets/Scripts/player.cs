using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public int health;
    public int fullHealth = 25;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //make sure when the game starts the players begins with full health
        health = fullHealth;
        healthBar.setMaxHealth(fullHealth);
    }

    // Update is called once per frame
    void Update()
    {
    }

    //call this to make the player take damage
    public void takeDamage(int damage)
    {
        health -= damage;
        healthBar.setHealth(health);
    }
}
