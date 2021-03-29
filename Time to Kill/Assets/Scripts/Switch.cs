using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    //is the player colliding with the other object?
    private bool colliding = false;
    //is the switch on?
    private bool on = false;
    public Sprite switchOn;
    public Sprite switchOff;
    private SpriteRenderer currentSwitch;
    public int order;

    //player (edit in inspector)
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentSwitch = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player is pressing J and colliding with the object
        if (Input.GetKeyDown(KeyCode.J) && colliding)
        {
            UpdateState();
        }
    }

    private void UpdateState()
    {
        on = !on;
        if(on)
        {
            currentSwitch.sprite = switchOn;
        }
        else
        {
            currentSwitch.sprite = switchOff;
        }
    }

    public bool isOn()
    {
        return on;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            colliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            colliding = false;
        }
    }
}
