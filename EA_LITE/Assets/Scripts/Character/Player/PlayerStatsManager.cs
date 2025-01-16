using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : CharacterStatsManager
{
    // PlayerManager player;

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
        CalculateHealthBasedOnVitalityLevel(vitality);
        CalculateStaminaBasedOnEnduranceLevel(endurance);
    }

    public void SetNewMaxHealthValue(int oldVitality, int newVitality)
    {
        maxHealth = CalculateHealthBasedOnVitalityLevel(newVitality);
        PlayerUIManager.instance.playerUIHudManager.SetMaxHealthValue(maxHealth);
        CurrentHealth = maxHealth;
    }

    public void SetNewMaxStaminaValue(int oldEndurance, int newEndurance)
    {
        maxStamina = CalculateStaminaBasedOnEnduranceLevel(newEndurance);
        PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(maxStamina);
        CurrentStamina = maxStamina;
    }
}
