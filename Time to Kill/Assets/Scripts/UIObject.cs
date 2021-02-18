using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIObject : MonoBehaviour
{
    //object info
    public string[] info;
    //is the object selected?
    private Vector2 objectSlotSelected;
    //slot selected
    public NavigateInventory slotSelected;
    //corresponding object
    public ObjectInteraction obj;
    //position of the object in the grid
    private Vector2 pos = new Vector2(-1, -1);
    //is this object selected?
    private bool isSelected;
    //text to display
    public string infoText;
    //text display
    public TextMeshProUGUI textDisplay;
    //replace
    private string trim;
    //length
    private int length;

    public void SetUp()
    {
        pos = obj.GetObjectPosition();
        Debug.Log("position of object " + obj + pos);
    }

    // Update is called once per frame
    void Update()
    {
        objectSlotSelected = slotSelected.getSelected();
        isSelected = objectSlotSelected == pos;
        if(isSelected)
        {
            textDisplay.text = "";
            textDisplay.text += infoText;
            length = infoText.Length;
        }
    }
    public bool getObjectSelected()
    {
        return isSelected;
    }
}
