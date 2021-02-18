using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    //object tag!
    private string uiObjTag;
    //equipped object
    public GameObject objToEquip;
    //image to set the equipped object to
    private Image uiSprite;


    public void SetUp(Vector2 getPos)
    {
        pos = getPos;
        uiSprite = transform.gameObject.GetComponent<Image>();
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
        }
    }
    public bool getObjectSelected()
    {
        return isSelected;
    }

    public void setUIObjTag(string set)
    {
        uiObjTag = set;
    }


    private void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("J")))
        {
            if(isSelected)
            {
                EquippedObject access = (EquippedObject)objToEquip.GetComponent(typeof(EquippedObject));
                access.setEquippedTag(uiObjTag);
                access.setSprite(uiSprite.sprite);
            }
        }
    }
}
