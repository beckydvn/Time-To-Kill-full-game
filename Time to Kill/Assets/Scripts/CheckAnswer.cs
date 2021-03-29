using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAnswer : MonoBehaviour
{
    private GameObject[] getSwitches;
    private GameObject[] getNumSwitches;
    private Switch[] switches;
    private NumberSwitch[] numSwitches;
    private GameObject[] switchPanels;

    public bool[] switchesAnswer;
    public int[] numSwitchesAnswer;
    private bool correct = false;

    private bool colliding = false;

    // Start is called before the first frame update
    void Start()
    {
        getSwitches = GameObject.FindGameObjectsWithTag("Switch");
        getNumSwitches = GameObject.FindGameObjectsWithTag("Number Switch");
        switchPanels = GameObject.FindGameObjectsWithTag("Switch Panel");
        switches = new Switch[getSwitches.Length];
        numSwitches = new NumberSwitch[getSwitches.Length];
        for (int i = 0; i < getSwitches.Length; i++)
        {
            switches[i] = (Switch)getSwitches[i].GetComponent(typeof(Switch));
            numSwitches[i] = (NumberSwitch)getNumSwitches[i].GetComponent(typeof(NumberSwitch));
        }
        //sort (taken from geeks for geeks)

        int n = getSwitches.Length;
        for (int i = 1; i < n; ++i)
        {
            int key = switches[i].order;
            Switch original = switches[i];
            int j = i - 1;
            while (j >= 0 && switches[j].order > key)
            {
                switches[j + 1] = switches[j];
                j = j - 1;
            }
            switches[j + 1] = original;
        }

        for (int i = 1; i < n; ++i)
        {
            int key = numSwitches[i].order;
            NumberSwitch original = numSwitches[i];
            int j = i - 1;
            while (j >= 0 && numSwitches[j].order > key)
            {
                numSwitches[j + 1] = numSwitches[j];
                j = j - 1;
            }
            numSwitches[j + 1] = original;
        }
    }

    private void Update()
    {
        
        //if the player is pressing J and colliding with the object
        if (Input.GetKeyDown(KeyCode.J) && colliding)
        {
          Results();
        }
    }
    public void Results()
    {
        correct = true;
        for (int i = 0; i < switches.Length; i++)
        {
            if(switches[i].isOn() != switchesAnswer[i] || numSwitches[i].intSelected() != numSwitchesAnswer[i])
            {
                correct = false;
                break;
            }
        }
        if(correct)
        {
            for (int i = 0; i < switchPanels.Length; i++)
            {
                Destroy(switchPanels[i]);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            colliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            colliding = false;
        }
    }
}
