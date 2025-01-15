using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BASE CLASS FOR ALL STAT BAR SCRIPTS

public class UI_StatBar : MonoBehaviour
{
    private Slider slider;
    // SECONDARY BAR TO SEE HOW MUCH HEALTH CONSUMED BY DAMAGE

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // was int
    public virtual void SetStat(float newValue)
    {
        slider.value = newValue;
    }

    // was int
    public virtual void SetMaxStat(float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }
}
