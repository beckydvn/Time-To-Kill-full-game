using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TakeObjectNPC : MonoBehaviour
{
    //is the dialog activated?
    private bool activateDialog;
    //is the player colliding with the other object?
    private bool colliding = false;
    //text display
    public TextMeshProUGUI textDisplay;
    //array of sentences (edit in inspector)
    public string[] sentences;
    //index of current sentence being displayed
    private int index = 0;
    //position of the player (to be frozen)
    Vector3 freeze;
    //player (edit in inspector)
    public GameObject player;
    //dialogue background (need to enable/disable)
    public CanvasRenderer dialogueBackground;
    //animator of the player
    private Animator anim;
    //does this object change the objective?
    public bool changeObjective;
    //optional objective change
    public string objectiveChange = "";
    //get the journal
    private GameObject getJournal;
    private UpdateJournal updateJournal;

    public string objToReceiveTag = "";
    public string rightText = "";
    public string wrongText = "";
    private GameObject getEquipped;
    private EquippedObject equipped;
    private bool firstEncounter = true;
    private UIObject uiObj;
    private bool receivedCorrect = false;
    private GameObject getInv;
    private ActivateInventory inventory;

    //object to spawn
    public GameObject spawnObj;
    //game manager
    private GameObject getGameManager;
    private CarryOverInfo gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //deactivate the text display and the background initially
        textDisplay.gameObject.SetActive(false);
        dialogueBackground.gameObject.SetActive(false);
        //get animator
        anim = player.GetComponent<Animator>();
        getJournal = GameObject.FindGameObjectWithTag("Journal");
        updateJournal = (UpdateJournal)getJournal.GetComponent(typeof(UpdateJournal));
        getEquipped = GameObject.FindGameObjectWithTag("Equipped Object");
        equipped = (EquippedObject)getEquipped.GetComponent(typeof(EquippedObject));
        spawnObj.gameObject.SetActive(false);
        getGameManager = GameObject.FindGameObjectWithTag("Game Manager");
        gameManager = (CarryOverInfo)getGameManager.GetComponent(typeof(CarryOverInfo));
        getInv = GameObject.FindGameObjectWithTag("Inventory");
        inventory = (ActivateInventory)getInv.GetComponent(typeof(ActivateInventory));
    }

    // Update is called once per frame
    void Update()
    {
        //if the player is pressing J and colliding with the object
        if (Input.GetKeyDown(KeyCode.J) && colliding && !inventory.getInvStatus())
        {
            //if no text has been read yet
            if (index == 0)
            {
                //activate the dialogue stage, show the text + background, and retrieve the player's position
                activateDialog = true;
                textDisplay.gameObject.SetActive(true);
                dialogueBackground.gameObject.SetActive(true);
                freeze = player.transform.position;
            }
            //read the next sentence
            UpdateText();
        }
        //while the dialog is activated, freeze the player's position
        if (activateDialog)
        {
            player.transform.position = freeze;
        }
    }

    private void UpdateText()
    {
        if(firstEncounter)
        {
            //read the next sentences
            if (index < sentences.Length)
            {
                textDisplay.text = "";
                textDisplay.text += sentences[index];
                index++;
            }
            //exit if all sentences have been read
            else
            {
                ExitInteraction();
            }
        }
        else
        {
            if(textDisplay.text == "")
            {
                if(equipped.getEquippedTag() == objToReceiveTag)
                {   
                    textDisplay.text += rightText;
                    GameObject[] uiObjects = GameObject.FindGameObjectsWithTag("UI Inventory Object");
                    foreach (var obj in uiObjects)
                    {
                        uiObj = (UIObject)obj.GetComponent(typeof(UIObject));
                        if(uiObj.getUIObjTag() == objToReceiveTag)
                        {
                            break;
                        }    
                    }
                    uiObj.setUsed();
                    equipped.UnEquip();
                    receivedCorrect = true;
                }
                else
                {
                    textDisplay.text += wrongText;
                }
            }
            else
            {
                    ExitInteraction();
            }
        }
    }

    private void ExitInteraction()
    {
        textDisplay.text = "";
        //deactivate the text display as well as the dialogue states
        textDisplay.gameObject.SetActive(false);
        dialogueBackground.gameObject.SetActive(false);
        activateDialog = false;
        //reset to 0 so the player can redo the interaction if they want!
        index = 0;
        anim.enabled = true;
        if (changeObjective)
        {
            updateJournal.setObjectiveText(objectiveChange);
        }
        if(firstEncounter)
        {
            firstEncounter = false;
        }
        if(receivedCorrect)
        {            
            spawnObj.SetActive(true);
            gameManager.newItemCollected(transform.tag);
            gameObject.GetComponent<TakeObjectNPC>().enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            colliding = true;
            anim.enabled = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            colliding = false;
            anim.enabled = true;
        }
    }
}

