using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHudManager : MonoBehaviour
{
    CharacterStatsManager characterStatsManager;

    [Header("Stat Bars")]
    [SerializeField] UI_StatBar healthBar;
    [SerializeField] UI_StatBar staminaBar;

    [SerializeField] UI_Image pickaxeCW;
    [SerializeField] UI_Image dynamiteCW;

    [SerializeField] UI_Image pickaxeSW;
    [SerializeField] UI_Image dynamiteSW;

    [SerializeField] private bool staminaBarUI = false;

    [Header("Boss Health Bar")]
    public Transform bossHealthBarParent;
    public GameObject bossHealthBarObject;

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

    public void SwapWeaponIcon(int weaponID)
    {
        if(weaponID == 1)
        {
            // set current weapon to dynamite
            dynamiteCW.gameObject.SetActive(true);
            pickaxeCW.gameObject.SetActive(false);

            //set stored weapon to pickaxe
            pickaxeSW.gameObject.SetActive(true);
            dynamiteSW.gameObject.SetActive(false);
        }
        else if(weaponID == 0)
        {
            // set current weapon to pickaxe
            pickaxeCW.gameObject.SetActive(true);
            dynamiteCW.gameObject.SetActive(false);

            //set stored weapon to dynamite
            dynamiteSW.gameObject.SetActive(true);
            pickaxeSW.gameObject.SetActive(false);
        }
    }
}
