using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedObject : MonoBehaviour
{
    //object tag!
    private string equippedTag;
    //sprite
    private Image equippedSprite;

    // Start is called before the first frame update
    void Start()
    {
        equippedSprite = transform.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setEquippedTag(string set)
    {
        equippedTag = set;
        Debug.Log(equippedTag);
    }

    public void setSprite(Sprite set)
    {
        equippedSprite.sprite = set;
    }
}
