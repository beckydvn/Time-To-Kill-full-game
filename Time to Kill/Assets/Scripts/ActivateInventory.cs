using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInventory : MonoBehaviour
{
    //get inventory gameobjects
    public GameObject inventoryCanvas;
    //activation status
    bool activationStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        inventoryCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activationStatus = !activationStatus;
            inventoryCanvas.gameObject.SetActive(activationStatus);
        }
    }
    
}
