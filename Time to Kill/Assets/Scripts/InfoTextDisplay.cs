using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoTextDisplay : MonoBehaviour
{
    private GameObject[] uiObjects;
    private bool noneSelected;
    //text display
    public TextMeshProUGUI textDisplay;
    // Start is called before the first frame update
    void Start()
    {
        uiObjects = GameObject.FindGameObjectsWithTag("UI Inventory Object");
    }

    // Update is called once per frame
    void Update()
    {
        noneSelected = true;
        foreach (GameObject element in uiObjects)
        {
            UIObject access = (UIObject)element.GetComponent(typeof(UIObject));
            if (access.getObjectSelected())
            {
                noneSelected = false;
            }
        }
        if(noneSelected)
        {
            textDisplay.text = "";
        }
    }
}
