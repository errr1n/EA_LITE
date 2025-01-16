using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;

public class PlayerUIHudManager : MonoBehaviour
{
    [SerializeField] UI_StatBar healthBar;
    [SerializeField] UI_StatBar staminaBar;

    // MAY NOT BE NECESSARY
    // public void RefreshHUD()
    // {
    //     healthBar.gameObject.SetActive(false);
    //     healthBar.gameObject.SetActive(true);

    //     staminaBar.gameObject.SetActive(false);
    //     staminaBar.gameObject.SetActive(true);
    // }

    public void SetNewHealthValue(float oldValue, float newValue)
    {
        healthBar.SetStat(newValue);
    }

    public void SetMaxHealthValue(int maxHealth)
    {
        healthBar.SetMaxStat(maxHealth);
    }

    public void SetNewStaminaValue(float oldValue, float newValue)
    {
        staminaBar.SetStat(newValue);
    }

    public void SetMaxStaminaValue(int maxStamina)
    {
        staminaBar.SetMaxStat(maxStamina);
    }



    // public void SetNewMaxHealthValue(int oldVitality, int newVitality)
    // {
    //     maxHealth = CalculateHealthBasedOnVitalityLevel(newVitality);
    //     PlayerUIManager.instance.playerUIHudManager.SetMaxHealthValue(maxHealth);
    //     CurrentHealth = maxHealth;
    // }

    // public void SetNewMaxStaminaValue(int oldEndurance, int newEndurance)
    // {
    //     maxStamina = CalculateStaminaBasedOnEnduranceLevel(newEndurance);
    //     PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(maxStamina);
    //     CurrentStamina = maxStamina;
    // }
}
