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
    private EquippedObject access;
    private Image uiSprite;
    //is the corresponding object equipped?
    private bool equipped = false;
    //text display
    public TextMeshProUGUI overlayText;
    //keep track if an object was used
    private bool used = false;
    public void SetUp(Vector2 getPos)
    {
        pos = getPos;
    }
    private void Start()
    {
        uiSprite = transform.gameObject.GetComponent<Image>();
        access = (EquippedObject)objToEquip.GetComponent(typeof(EquippedObject));
        //textClone = Instantiate(overlayText.gameObject);
        //textClone.transform.SetParent(gameObject.transform);
        overlayText.text = "";
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

        if(equipped)
        {
            //item was unequipped, remove overlay
            if(!access.getEquipped() && !access.getUsed())
            {
                overlayText.text = "";
                equipped = false;
            }
            //item was used, change overlay
            if (!access.getEquipped() && access.getUsed())
            {
                equipped = false;
                used = true;
                overlayText.text = "USED";
            }
            if(access.getEquipped() && !access.getUsed())
            {
                overlayText.text = "EQUIPPED";
            }
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
            //if object is selected and NO object is currently equipped AND this object was not previously used
            if(isSelected && !access.getEquipped() && !used)
            {
                access.setEquippedTag(uiObjTag);
                access.setSprite(uiSprite.sprite);
                access.setPos(pos);
                access.Equip();
                equipped = true;
            }
        }
    }
}
