using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInventory : MonoBehaviour
{
    //get the journal so we can know if the inventory is currently activated
    public GameObject getJournal;
    private ActivateJournal journal;
    //get inventory gameobjects
    public GameObject inventoryCanvas;
    //activation status
    private bool activationStatus = false;
    //original position of the player
    private Vector2 freeze;
    //player (needs to freeze the player transform)
    public GameObject player;
    //alpha of canvas to be hidden
    private CanvasGroup canvasAlpha;

    // Start is called before the first frame update
    void Start()
    {
        canvasAlpha = inventoryCanvas.GetComponent<CanvasGroup>();
        canvasAlpha.alpha = 0;
        journal = (ActivateJournal)getJournal.GetComponent(typeof(ActivateJournal));
    }

    public bool getInvStatus()
    {
        return activationStatus;
    }

    private void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("I")))
        {
            if(!journal.getJournalStatus())
            {
                activationStatus = !activationStatus;
                if (activationStatus)
                {
                    canvasAlpha.alpha = 1;
                }
                else
                {
                    canvasAlpha.alpha = 0;
                }
                //get the player's position to freeze it
                freeze = player.transform.position;
            }

        }
        if(activationStatus)
        {
            player.transform.position = freeze;
        }
    }
}
