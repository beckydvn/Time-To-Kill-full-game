using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasicObjectOrNPC : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        //deactivate the text display and the background initially
        textDisplay.gameObject.SetActive(false);
        dialogueBackground.gameObject.SetActive(false);
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
        //reset to 0 so the player can redo the interaction if they want!
        index = 0;
        anim.enabled = true;
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
