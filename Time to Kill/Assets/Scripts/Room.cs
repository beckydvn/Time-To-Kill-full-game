using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject virtualCamera;
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //whenever the player enters the camera region, set the camera to active
        if (other.CompareTag("Player"))
        {
            virtualCamera.SetActive(true);
            //Debug.Log(virtualCamera + " turned on");
        }
    }
    public virtual void OnTriggerExit2D(Collider2D other)
    {
        //turn the camera off when the player leaves the region
        if (other.CompareTag("Player"))
        {
            virtualCamera.SetActive(false);
            //Debug.Log(virtualCamera + " turned off");
        }
    }


    private void Start()
    {
        //each camera should be off initially
        virtualCamera.SetActive(false);
    }

}
