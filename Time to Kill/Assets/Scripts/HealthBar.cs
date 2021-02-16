using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider s;

    //sets the health for the char
    public void setHealth(int h)
    {
        s.value = h;
    }

    //sets the maximum health value for the char
    public void setMaxHealth(int h)
    {
        s.maxValue = h;
        s.value = h;
    }
}
