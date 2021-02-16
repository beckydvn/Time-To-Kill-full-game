using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Created by: Kiera McNeely
//Date Created: 2021-02-05
//Date Edited: 2021-02-05

public class playerMovement : MonoBehaviour
{
   
    public Rigidbody2D rigidbody;
    public float movementSpeed = 5f;
    public float h;
    public float v;
    public Animator animator;

    void Start()
    {
        //get access to rigid body
        rigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //create input for both x and y directions
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        
        /*
        //find total velocity of char
        var vel = rigidbody.velocity;

        //set what direction the char is moving
        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            //get last movement so player faces the correct way when they stop
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }

        //set speed so tranition happens when character moves
        animator.SetFloat("Speed", vel.sqrMagnitude);
        */
        
    }

    void FixedUpdate()
    {
        //move the player at specified move speed
        rigidbody.velocity = new Vector2(h * movementSpeed, v * movementSpeed);
    }
}
