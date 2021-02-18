using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : MonoBehaviour
{
    //object info
    public string[] info;
    //is the object selected?
    private Vector2 selected = new Vector2(0,0);
    //slot selected
    public NavigateInventory slotSelected;
    //corresponding object
    public ObjectInteraction obj;
    //position of the object in the grid
    private Vector2 pos;

    public void SetUp()
    {
        pos = obj.GetObjectPosition();
        Debug.Log("position of object " + obj + pos);
    }

    // Update is called once per frame
    void Update()
    {
        selected = slotSelected.getSelected();

        Debug.Log(selected == pos);
    }
}
