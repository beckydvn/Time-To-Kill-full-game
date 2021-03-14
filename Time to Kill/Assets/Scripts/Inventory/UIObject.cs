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
    private GameObject getSlotSelected;
    private NavigateInventory slotSelected;
    //corresponding object
    public ObjectInteraction obj;
    //position of the object in the grid
    private Vector2 pos = new Vector2(-1, -1);
    //is this object selected?
    private bool isSelected;
    //text to display
    public string infoText;    
    //text display
    private GameObject getTextDisplay;
    //text display
    private TextMeshProUGUI textDisplay;
    //object tag!
    private string uiObjTag;
    //equipped object
    //public GameObject objToEquip;
    //image to set the equipped object to
    private GameObject getAccess;
    private EquippedObject access;
    private Image uiSprite;
    //is the corresponding object equipped?
    private bool equipped = false;

    //is the object equippable?
    private bool equippable;

    //get the inventory
    private GameObject getInv;
    private ActivateInventory inv;

    public TextMeshProUGUI overlayText;
    //keep track if an object was used
    private bool used = false;
    public void SetUp(Vector2 getPos)
    {
        pos = getPos;
    }
    private void Start()
    {
        getSlotSelected = GameObject.FindGameObjectWithTag("Grid Slot Setup");
        slotSelected = (NavigateInventory)getSlotSelected.GetComponent(typeof(NavigateInventory));
        getInv = GameObject.FindGameObjectWithTag("Inventory");
        inv = (ActivateInventory)getInv.GetComponent(typeof(ActivateInventory));
        uiSprite = transform.gameObject.GetComponent<Image>();
        getAccess = GameObject.FindGameObjectWithTag("Equipped Object");
        access = (EquippedObject)getAccess.GetComponent(typeof(EquippedObject));
        getTextDisplay = GameObject.FindGameObjectWithTag("UI Object Info Text");
        textDisplay = (TextMeshProUGUI)getTextDisplay.GetComponent(typeof(TextMeshProUGUI));
        overlayText.text = "";
        DontDestroyOnLoad(transform.gameObject);
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
    public void setEquippable(bool set)
    {
        equippable = set;
    }

    private void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("J")))
        {
            //if object is selected and NO object is currently equipped AND this object was not previously used AND
            //this object is equippable AND the inventory is open
            if(isSelected && !access.getEquipped() && !used && equippable && inv.getInvStatus())
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
