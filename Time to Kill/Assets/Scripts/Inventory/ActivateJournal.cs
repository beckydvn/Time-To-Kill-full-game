using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateJournal : MonoBehaviour
{
    //get the inventory so we can know if the inventory is currently activated
    public GameObject getInv;
    private ActivateInventory inventory;
    //get journal gameobjects
    public GameObject journalCanvas;
    //is the journal activated?
    private bool activationStatus = false;
    //original position of the player
    private Vector2 freeze;
    //player (needs to freeze the player transform)
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        journalCanvas.gameObject.SetActive(false);
        inventory = (ActivateInventory)getInv.GetComponent(typeof(ActivateInventory));
    }

    public bool getJournalStatus()
    {
        return activationStatus;
    }

    private void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("tab")))
        {
            if(!inventory.getInvStatus())
            {
                activationStatus = !activationStatus;
                journalCanvas.gameObject.SetActive(activationStatus);

                //get the player's position to freeze it
                freeze = player.transform.position;
            }

        }
        if (activationStatus)
        {
            player.transform.position = freeze;
        }
    }
}
