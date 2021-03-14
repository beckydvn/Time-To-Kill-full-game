using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CollectableObject : MonoBehaviour
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

    //list of stored objects
    List<string> inventoryList = new List<string>();
    //is the object EQUIPPABLE?
    public bool equippable;
    //grid to add objects to in inventory
    private GameObject gridSetup;
    //layout of the grid
    private GridLayoutGroup objectGrid;
    //object to add to inventory
    public GameObject inventoryObject;
    //number of slots taken
    private int numSlots = 0;
    //row and column position
    private int rowPos;
    private int colPos;
    //position of the object in the invent ory
    private Vector2 inventoryPos;
    //inventory object to get the row/col number
    private GameObject getInvScript;
    private NavigateInventory invScript;
    //animator of the player
    private Animator anim;

    //game manager
    private GameObject getGameManager;
    private CarryOverInfo gameManager;

    // Start is called before the first frame update
    void Start()
    {        
        getGameManager = GameObject.FindGameObjectWithTag("Game Manager");
        gameManager = (CarryOverInfo)getGameManager.GetComponent(typeof(CarryOverInfo));
        if(gameManager.itemCollected(transform.tag))
        {
            Destroy(gameObject);
        }
        gridSetup = GameObject.FindGameObjectWithTag("Grid Sprite Setup");
        getInvScript = GameObject.FindGameObjectWithTag("Grid Slot Setup");
        invScript = (NavigateInventory)getInvScript.GetComponent(typeof(NavigateInventory));
        //deactivate the text display and the background initially
        textDisplay.gameObject.SetActive(false);
        dialogueBackground.gameObject.SetActive(false);
        //instantiate object grid
        objectGrid = gridSetup.GetComponent<GridLayoutGroup>();
        //get animator
        anim = player.GetComponent<Animator>();


   }

    // Update is called once per frame
    void Update()
    {
        //if the player is pressing J and colliding with the object
        if (Input.GetKeyDown(KeyCode.J) && colliding)
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

    private void ExitInteraction()
    {
        //deactivate the text display as well as the dialogue states
        textDisplay.gameObject.SetActive(false);
        dialogueBackground.gameObject.SetActive(false);
        activateDialog = false;
        //destroy the object once collected
        Destroy(this.gameObject);
        //add to inventory (USE A GENERAL MOD CALCULATION HERE TO TELL WHAT POSITION THE OBJECT IS IN; THEN, YOU CAN
        //LINK IT TO THE OBJECT THROUGH A VARIABLE OR COMPONENT. THEN, IN THE OBJECT YOU CAN CHECK IF THE POSITION IS EQUAL
        //TO THE CURRENT SELECTED POSITION, AND IF IT IS, DISPLAY THE OBJECT'S TEXT.

        numSlots = objectGrid.transform.childCount;
        rowPos = numSlots / invScript.getCols();
        colPos = numSlots % invScript.getCols();
        inventoryPos = new Vector2(rowPos, colPos);

        GameObject nextObject = Instantiate(inventoryObject);
        nextObject.transform.SetParent(objectGrid.transform);
        UIObject access = (UIObject)nextObject.GetComponent(typeof(UIObject));
        //set position
        access.SetUp(inventoryPos);
        //pass on tag
        access.setUIObjTag(transform.tag);
        access.setEquippable(equippable);
        anim.enabled = true;

        gameManager.newItemCollected(transform.tag);
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
