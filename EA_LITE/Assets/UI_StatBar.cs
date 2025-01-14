using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatBar : MonoBehaviour
{
    private Slider slider;
    // SECONDARY BAR TO SEE HOW MUCH HEALTH CONSUMED BY DAMAGE

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public virtual void SetStat()
    {

    }

    public virtual void SetMaxStat()
    {
        
    }
}
