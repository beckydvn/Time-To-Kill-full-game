using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    //holds the combo the user typed in
    private string combo = "";
    //alphabet
    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    //is the player currently attacking? (if not, the boss is)
    private bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(combo);
    }

    private void OnGUI()
    {
        foreach (char letter in alphabet)
        {
            if (Event.current.Equals(Event.KeyboardEvent(letter.ToString())))
            {
                combo += letter;
            }
        }
    }
}
