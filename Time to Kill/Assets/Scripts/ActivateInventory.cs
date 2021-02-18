using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInventory : MonoBehaviour
{
    //get inventory gameobjects
    public GameObject inventoryCanvas;
    //activation status
    bool activationStatus = false;
    //original position of the player
    private Vector2 freeze;
    //player (needs to freeze the player transform)
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        inventoryCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activationStatus = !activationStatus;
            inventoryCanvas.gameObject.SetActive(activationStatus);        
            //get the player's position to freeze it
            freeze = player.transform.position;
        }
        if(activationStatus)
        {
            player.transform.position = freeze;
        }

    }
    
}
