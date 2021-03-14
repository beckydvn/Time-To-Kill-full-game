using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedObject : MonoBehaviour
{
    //object tag!
    private string equippedTag = "empty";
    //sprite
    private Image equippedSprite;
    //is an object equipped?
    private bool isEquipped = false;
    //image in the slot
    private Image image;
    //inventory (to get status)
    private GameObject inventoryStatus;
    private ActivateInventory accessInv;
    private bool inventoryActivated;
    //position of the corresponding UI object
    private Vector2 pos;
    //has the object been used?
    private bool used = false;

    // Start is called before the first frame update
    void Start()
    {
        equippedSprite = transform.gameObject.GetComponent<Image>();
        image = GetComponent<Image>();
        image.enabled = false;
        inventoryStatus = GameObject.FindGameObjectWithTag("Inventory");
        accessInv = inventoryStatus.GetComponent<ActivateInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        inventoryActivated = accessInv.getInvStatus();
    }

    public bool getUsed()
    {
        return used;
    }
    public bool getEquipped()
    {
        return isEquipped;
    }

    private void OnGUI()
    {
        //player uses the equipped item when the inventory is not activated
        if (Event.current.Equals(Event.KeyboardEvent("U")))
        {
            UnEquip();
            used = true;
        }
        //player unequips the item
        if (Event.current.Equals(Event.KeyboardEvent("X")))
        {
            UnEquip();
            used = false;
        }
    }

    private void UnEquip()
    {
        if (isEquipped && !inventoryActivated)
        {
            isEquipped = false;
            image.enabled = false;
            setEquippedTag("empty");
        }
    }

    public void Equip()
    {
        image.enabled = true;
        isEquipped = true;
        used = false;
        //call the setup functions
        //find the UI object with the corresponding position
        //instantiate object with the UI obj as its parent
        //Overlay "equipped"
        //used or equipped objects cannot be equipped; send bool to UI object
        //prob easier to do most of this stuff in UI object update
    }

    public void setEquippedTag(string set)
    {
        equippedTag = set;   
    }

    public void setSprite(Sprite set)
    {
        equippedSprite.sprite = set; 
    }

    public void setPos(Vector2 set)
    {
        pos = set;
    }
}
