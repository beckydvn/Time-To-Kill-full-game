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

    //timer for time bonus
    private GameObject getTimer;
    private countdownTimer timer;

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
        Debug.Log(equippedTag);
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
            used = true;
            switch(equippedTag)
            {
                case "Time Bonus":
                    {
                        getTimer = GameObject.FindGameObjectWithTag("Timer");
                        timer = (countdownTimer)getTimer.GetComponent(typeof(countdownTimer));
                        timer.setTimeLeft(timer.getTimeLeft() + 20);
                        break;
                    }

            }
            UnEquip();
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
