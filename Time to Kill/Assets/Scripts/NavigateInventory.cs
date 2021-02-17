using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigateInventory : MonoBehaviour
{
    //holds slots
    public GameObject[,] slots;
    //slot prefab reference
    public GameObject slot;
    //number of columns
    private int cols = 3;
    //number of rows
    private int rows = 4;
    //directions to be used
    public string up;
    public string down;
    public string left;
    public string right;
    //player (needs to freeze the player transform)
    public GameObject player;
    //tells if slots are full
    private bool[,] isFull;
    //holds movement
    private float h, v;
    //position of selected
    private Vector2 selected = new Vector2(0, 0);
    //previous position of selected
    private Vector2 prevSelected = new Vector2(0, 0);
    //button pressed sprite
    public Sprite pressed;
    //button up sprite
    public Sprite buttonUp;
    //has the selected slot changed?
    private bool changedSelected;
    //original position of the player
    private Vector2 freeze;

    // Start is called before the first frame update
    void Start()
    {
        //clone the slots
        GameObject slotclone;
        //get the grid component
        GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();
        //instantiate arrays
        isFull = new bool[rows, cols];
        slots = new GameObject[rows, cols];
        //get the player's position to freeze it
        freeze = player.transform.position;

        //set up bools and clones
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                isFull[i, j] = false;
                slotclone = Instantiate(slot);
                slotclone.transform.SetParent(gridLayoutGroup.transform);
                //added to the GameObject array for later access
                slots[i, j] = slotclone;
            }
        }
        //destroy the original that was cloned
        Destroy(slot);
        //set the first slot to selected
        ChangeSprite(selected, prevSelected);
    }

    private void Update()
    {
        player.transform.position = freeze;
    }
    
    private void ChangeSprite(Vector2 prevSelected, Vector2 selected)
    {
        //unselect previous slot and select current slot (graphically)
        Image unselect = slots[(int)prevSelected.x, (int)prevSelected.y].GetComponent<Image>();
        unselect.sprite = buttonUp;
        Image select = slots[(int)selected.x, (int)selected.y].GetComponent<Image>();
        select.sprite = pressed;
    }


    private void OnGUI()
    {
        prevSelected = new Vector2((int)selected.x, (int)selected.y);

        if (Event.current.Equals(Event.KeyboardEvent(up)))
        {
            //isFull[(int)selected.x, (int)selected.y] = false;
            if (selected.x > 0)
            {
                changedSelected = true;     
                selected.x -= 1;
            }
        }
        if (Event.current.Equals(Event.KeyboardEvent(down)))
        {
            //isFull[(int)selected.x, (int)selected.y] = false;
            if (selected.x < rows - 1)
            {
                changedSelected = true;
                selected.x += 1;
            }
        }
        if (Event.current.Equals(Event.KeyboardEvent(right)))
        {
            //isFull[(int)selected.x, (int)selected.y] = false;
            if (selected.y < cols - 1)
            {
                changedSelected = true;
                selected.y += 1;
            }
        }
        if (Event.current.Equals(Event.KeyboardEvent(left)))
        {
            //isFull[(int)selected.x, (int)selected.y] = false;
            if (selected.y > 0)
            {
                changedSelected = true;
                selected.y -= 1;
            }
        }

        if(changedSelected)
        {
            ChangeSprite(prevSelected, selected);
        }

        //isFull[(int)selected.x, (int)selected.y] = true;
    }
}
