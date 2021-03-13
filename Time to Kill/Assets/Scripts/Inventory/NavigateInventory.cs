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
    public int cols = 3;
    //number of rows
    public int rows = 4;
    //directions to be used
    public string up;
    public string down;
    public string left;
    public string right;
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
    //get inventory (is the inventory active?)
    private GameObject getInv;
    private ActivateInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        getInv = GameObject.FindGameObjectWithTag("Inventory");
        inventory = (ActivateInventory)getInv.GetComponent(typeof(ActivateInventory));

        //clone the slots
        GameObject slotclone;
        //get the grid component
        GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();
        //instantiate arrays
        isFull = new bool[rows, cols];
        slots = new GameObject[rows, cols];

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
    
    public int getRows()
    {
        return rows;
    }   
    public int getCols()
    {
        return cols;
    }
    public Vector2 getPrevSelected()
    {
        return prevSelected;
    }
    public Vector2 getSelected()
    {
        return selected;
    }
    public void setSelected(Vector2 set)
    {
        selected = set;
    }

    public void ChangeSprite(Vector2 prevSelected, Vector2 selected)
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
        if(inventory.getInvStatus())
        {
            if (Event.current.Equals(Event.KeyboardEvent(up)))
            {
                if (selected.x > 0)
                {
                    changedSelected = true;     
                    selected.x -= 1;
                }
            }
            if (Event.current.Equals(Event.KeyboardEvent(down)))
            {
                if (selected.x < rows - 1)
                {
                    changedSelected = true;
                    selected.x += 1;
                }
            }
            if (Event.current.Equals(Event.KeyboardEvent(right)))
            {
                if (selected.y < cols - 1)
                {
                    changedSelected = true;
                    selected.y += 1;
                }
            }
            if (Event.current.Equals(Event.KeyboardEvent(left)))
            {
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
        }


    }
}
