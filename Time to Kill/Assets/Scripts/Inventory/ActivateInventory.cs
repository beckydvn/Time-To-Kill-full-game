using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInventory : MonoBehaviour
{
    //get the journal so we can know if the inventory is currently activated
    private GameObject getJournal;
    private ActivateJournal journal;
    //activation status
    private bool activationStatus = false;
    //original position of the player
    private Vector2 freeze;
    //player (needs to freeze the player transform)
    public GameObject player;
    //alpha of canvas to be hidden
    private CanvasGroup canvasAlpha;
    //get inventory grid
    public GameObject getNav;
    private NavigateInventory nav;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        canvasAlpha = transform.gameObject.GetComponent<CanvasGroup>();
        canvasAlpha.alpha = 0;
        instantiateNew();
    }

    void instantiateNew()
    {
        getJournal = GameObject.FindGameObjectWithTag("Journal");
        journal = (ActivateJournal)getJournal.GetComponent(typeof(ActivateJournal));
        player = GameObject.FindGameObjectWithTag("Player");
        nav = (NavigateInventory)getNav.GetComponent(typeof(NavigateInventory));
    }

    public bool getInvStatus()
    {
        return activationStatus;
    }

    private void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("I")))
        {
            if(journal == null || player == null)
            {
                instantiateNew();
            }
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
                nav.setSelected(new Vector2(0, 0));
                nav.ChangeSprite(nav.getPrevSelected(), new Vector2(0, 0));
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
