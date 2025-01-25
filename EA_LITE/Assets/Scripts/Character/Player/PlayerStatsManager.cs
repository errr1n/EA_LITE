using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : CharacterStatsManager
{
    // PlayerManager player;

    // [SerializeField] float _currentHealth = 0;
    // public float CurrentHealth{
    //     get{return _currentHealth;}
    //     set{
    //         // UPDATES HEALTH UI BAR WHEN HEALTH CHANGES 
    //         PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue(_currentHealth, value);
    //         _currentHealth = value;
    //         // Debug.Log("CURRENT HEALTH: " + _currentHealth);
    //     }
    // }

    protected override void Awake()
    {
        base.Awake();

        // player = GetComponent<PlayerManager>();

        // CalculateHealthBasedOnVitalityLevel();
    }

    protected override void Start()
    {
        base.Start();

        // WHY CALCULATE THESE VALUES HERE?
        // IF WE MAKE A CHARACTER CREATION MENU AND SET STATS THERE, THIS WILL BE CALCULATED THERE
        CalculateHealthBasedOnVitalityLevel(currentVitality);
        CalculateStaminaBasedOnEnduranceLevel(currentVitality);
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
