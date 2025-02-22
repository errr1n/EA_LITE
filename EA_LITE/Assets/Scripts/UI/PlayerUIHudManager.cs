using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;

public class PlayerUIHudManager : MonoBehaviour
{
    CharacterStatsManager characterStatsManager;
    // public PlayerManager player;
    // public PlayerEquipmentManager playerEquipmentManager;

    [Header("Stat Bars")]
    [SerializeField] UI_StatBar healthBar;
    [SerializeField] UI_StatBar staminaBar;

    [SerializeField] UI_Image pickaxeCW;
    [SerializeField] UI_Image dynamiteCW;

    [SerializeField] UI_Image pickaxeSW;
    [SerializeField] UI_Image dynamiteSW;

    [SerializeField] private bool staminaBarUI = false;

    private bool cwPickaxe = true;
    

    [Header("Boss Health Bar")]
    public Transform bossHealthBarParent;
    public GameObject bossHealthBarObject;

    // public int currentIndex = 0;

    private void Awake()
    {
        characterStatsManager = GetComponent<CharacterStatsManager>();
        // playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
    }

    private void Update()
    {
        // TURNS OFF STAMINA BAR ON HUD
        staminaBar.gameObject.SetActive(staminaBarUI);

        // dynamiteCW.gameObject.SetActive(true);
        // pickaxeCW.gameObject.SetActive(false);
        // SwapWeaponIcon();
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

    // public void SetNewBossHealthValue(float newValue)
    // {
    //     healthBar.SetStat(newValue);
    // }



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

    public void SwapWeaponIcon()
    {
        if(cwPickaxe == true)
        {
            cwPickaxe = false;
            // set current weapon to dynamite
            dynamiteCW.gameObject.SetActive(true);
            pickaxeCW.gameObject.SetActive(false);

            //set stored weapon to pickaxe
            pickaxeSW.gameObject.SetActive(true);
            dynamiteSW.gameObject.SetActive(false);
        }
        else if(cwPickaxe == false)
        {
            cwPickaxe = true;
            // set current weapon to pickaxe
            pickaxeCW.gameObject.SetActive(true);
            dynamiteCW.gameObject.SetActive(false);

            //set stored weapon to dynamite
            dynamiteSW.gameObject.SetActive(true);
            pickaxeSW.gameObject.SetActive(false);
        }
    }
}
