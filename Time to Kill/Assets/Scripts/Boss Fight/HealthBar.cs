using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider s;

    //sets the health for the char
    public void setHealth(float h)
    {
        s.value = h;
    }

    //sets the maximum health value for the char
    public void setMaxHealth(float h)
    {
        s.maxValue = h;
        s.value = h;
    }
    //get the health
    public float getHealth()
    {
        return s.value;
    }
    public float getMaxHealth()
    {
        return s.maxValue;
    }
}
