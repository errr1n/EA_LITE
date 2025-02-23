using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHudManager : MonoBehaviour
{
    CharacterStatsManager characterStatsManager;

    [Header("Stat Bars")]
    [SerializeField] UI_StatBar healthBar;
    [SerializeField] UI_StatBar staminaBar;

    [Header("Current Weapon Icons")]
    [SerializeField] UI_Image pickaxeCW;
    [SerializeField] UI_Image dynamiteCW;

    [Header("Stored Weapon Icons")]
    [SerializeField] UI_Image pickaxeSW;
    [SerializeField] UI_Image dynamiteSW;

    [Header("Boss Health Bar")]
    public Transform bossHealthBarParent;
    public GameObject bossHealthBarObject;

    // toggle on or off stamina bar UI
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

    // HEALTH
    public void SetNewHealthValue(float newValue)
    {
        // adjusts the health bar slider to the newValue
        healthBar.SetStat(newValue);
    }

    public void SetMaxHealthValue(int maxHealth)
    {
        // adjusts the max health bar slider value to the maxHealth value
        healthBar.SetMaxStat(maxHealth);
    }

    public void SetNewMaxHealthValue(int oldVitality, int newVitality)
    {
        // calculate health based on passed through vitality amount
        characterStatsManager.maxHealth = characterStatsManager.CalculateHealthBasedOnVitalityLevel(newVitality);
        // set max health value
        SetMaxHealthValue(characterStatsManager.maxHealth);
        // set current health to max health
        characterStatsManager.CurrentHealth = characterStatsManager.maxHealth;
    }

    // STAMINA - NOT USED
    public void SetNewStaminaValue(float oldValue, float newValue)
    {
        staminaBar.SetStat(newValue);
    }

    public void SetMaxStaminaValue(int maxStamina)
    {
        staminaBar.SetMaxStat(maxStamina);
    }

    public void SetNewMaxStaminaValue(int oldEndurance, int newEndurance)
    {
        characterStatsManager.maxStamina = characterStatsManager.CalculateStaminaBasedOnEnduranceLevel(newEndurance);
        PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(characterStatsManager.maxStamina);
        characterStatsManager.CurrentStamina = characterStatsManager.maxStamina;
    }

    // SWAP WEAPON ICONS ON PLAYER HUD
    public void SwapWeaponIcon(int weaponID)
    {
        if(weaponID == 1)
        {
            // set current weapon to dynamite
            dynamiteCW.gameObject.SetActive(true); // turn on current weapon dynamite img
            pickaxeCW.gameObject.SetActive(false);  // turn off current weapon pickaxe img

            //set stored weapon to pickaxe
            pickaxeSW.gameObject.SetActive(true);   // turn on stored weapon pickaxe img
            dynamiteSW.gameObject.SetActive(false); // turn off stored weapon dynamite img
        }
        else if(weaponID == 0)
        {
            // set current weapon to pickaxe
            pickaxeCW.gameObject.SetActive(true);   // turn on current weapon pickaxe img
            dynamiteCW.gameObject.SetActive(false); // turn off current weapon dynamite img

            //set stored weapon to dynamite
            dynamiteSW.gameObject.SetActive(true);  // turn on stored weapon dynamite img
            pickaxeSW.gameObject.SetActive(false);  // turn off stored weapon pickaxe img
        }
    }
}
