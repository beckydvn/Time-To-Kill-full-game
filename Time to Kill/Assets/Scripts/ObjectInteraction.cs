using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
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
    //is the object COLLECTABLE?
    public bool collectable;
    //grid to add objects to in inventory
    public GameObject gridSetup;
    //layout of the grid
    private GridLayoutGroup objectGrid;
    //object to add to inventory
    public GameObject inventoryObject;

    // Start is called before the first frame update
    void Start()
    {
        //deactivate the text display and the background initially
        textDisplay.gameObject.SetActive(false);
        dialogueBackground.gameObject.SetActive(false);

        //instantiate object grid
        objectGrid = gridSetup.GetComponent<GridLayoutGroup>();

    }

    // Update is called once per frame
    void Update()
    {
        //if the player is pressing F and colliding with the object
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
        if(collectable)
        {
            //destroy the object once collected
            Destroy(this.gameObject);
            //add to inventory
            GameObject nextObject = Instantiate(inventoryObject);
            nextObject.transform.SetParent(objectGrid.transform);

            //inventoryList.Add(this.ToString());
            //Debug.Log(inventoryList);
        }
        else
        {
            //player can interact with it again
            index = 0;
        }


    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.transform.position.y < transform.position.y)
        {
            colliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            colliding = false;
        }
    }
}
