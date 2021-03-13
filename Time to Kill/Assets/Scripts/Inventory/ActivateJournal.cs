using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateJournal : MonoBehaviour
{
    //get the inventory so we can know if the inventory is currently activated
    private GameObject getInv;
    private ActivateInventory inventory;
    //is the journal activated?
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
        canvasAlpha = this.gameObject.GetComponent<CanvasGroup>();
        canvasAlpha.alpha = 0;
        getInv = GameObject.FindGameObjectWithTag("Inventory");
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
                if(activationStatus)
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
        if (activationStatus)
        {
            player.transform.position = freeze;
        }
    }
}
