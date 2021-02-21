using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{

    public Slider s;
    private void Start()
    {
        
    }

    //sets the health for the char
    public void setCharge(float h)
    {
        s.value = h;
    }

    //sets the maximum health value for the char
    public void setMaxCharge(float h)
    {
        s.maxValue = h;
        s.value = h;
    }
    //get the health
    public float getCharge()
    {
        return s.value;
    }
    public float getMaxCharge()
    {
        return s.maxValue;
    }
}

