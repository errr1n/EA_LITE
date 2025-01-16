using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BASE CLASS FOR ALL STAT BAR SCRIPTS

public class UI_StatBar : MonoBehaviour
{
    private Slider slider;
    private RectTransform rectTransform;

    // SCALE STAT BAR DEPENDING ON STATS
    [Header("BAR OPTIONS")]
    [SerializeField] protected bool scaledBarLengthWidthStats = true;
    [SerializeField] protected float widthScaleMultiplier = 1;

    // SECONDARY BAR TO SEE HOW MUCH HEALTH CONSUMED BY DAMAGE

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
        rectTransform = GetComponent<RectTransform>();
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

        if(scaledBarLengthWidthStats)
        {
            // SCALE THE TRANSFORM OF THIS OBJECT
            // ONLY CHANGE THE WIDTH OF STATS BAR
            rectTransform.sizeDelta = new Vector2(maxValue * widthScaleMultiplier, rectTransform.sizeDelta.y);

            // RESETS THE POSITION OF THE BARS BASED ON THEIR LAYOUT GROUP SETTINGS
            // PlayerUIManager.instance.playerUIHudManager.RefreshHUD();
        }
    }
}
