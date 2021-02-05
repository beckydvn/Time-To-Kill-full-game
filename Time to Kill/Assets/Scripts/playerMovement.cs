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
    }

    void FixedUpdate()
    {
        //move the player at specified move speed
        rigidbody.velocity = new Vector2(h * movementSpeed, v * movementSpeed);
    }
}
