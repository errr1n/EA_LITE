using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;

public class PlayerUIHudManager : MonoBehaviour
{
    CharacterStatsManager characterStatsManager;

    [SerializeField] UI_StatBar healthBar;
    [SerializeField] UI_StatBar staminaBar;

    [SerializeField] private bool staminaBarUI = false;

    private void Awake()
    {
        characterStatsManager = GetComponent<CharacterStatsManager>();
    }

    private void Update()
    {
        // TURNS OFF STAMINA BAR ON HUD
        staminaBar.gameObject.SetActive(staminaBarUI);
    }

    // MAY NOT BE NECESSARY
    public void RefreshHUD()
    {
        healthBar.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(true);

        // staminaBar.gameObject.SetActive(false);
        // staminaBar.gameObject.SetActive(true);
    }

    public void SetNewHealthValue(float newValue)
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



    public void SetNewMaxHealthValue(int oldVitality, int newVitality)
    {
        characterStatsManager.maxHealth = characterStatsManager.CalculateHealthBasedOnVitalityLevel(newVitality);
        PlayerUIManager.instance.playerUIHudManager.SetMaxHealthValue(characterStatsManager.maxHealth);
        characterStatsManager.CurrentHealth = characterStatsManager.maxHealth;
    }

    public void SetNewMaxStaminaValue(int oldEndurance, int newEndurance)
    {
        characterStatsManager.maxStamina = characterStatsManager.CalculateStaminaBasedOnEnduranceLevel(newEndurance);
        PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(characterStatsManager.maxStamina);
        characterStatsManager.CurrentStamina = characterStatsManager.maxStamina;
    }
}
