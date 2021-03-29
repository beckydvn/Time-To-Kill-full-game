using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberSwitch : MonoBehaviour
{

    //is the player colliding with the other object?
    private bool colliding = false;
    //display number selected
    public TextMeshProUGUI textDisplay;
    //list of numbers
    public int[] nums;
    private int index = 0;
    //order
    public int order;

    //player (edit in inspector)
    public GameObject player;

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
        index++;
        if(index >= nums.Length)
        {
            index = 0;
        }
        textDisplay.text = nums[index].ToString();
    }

    public int intSelected()
    {
        return nums[index];
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
