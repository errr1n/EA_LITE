using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    CharacterManager character;

    [Header("STAMINA REGENERATION")]
    private float staminaRegenerationTimer = 0;
    private float staminaTickTimer = 0;
    [SerializeField] float staminaRegenerationDelay = 2;
    [SerializeField] float staminaRegenerationAmount = 2;

    [Header("CHARACTER STAMINA STATS")]
    public int endurance = 1;
    public int maxStamina = 0;

    private float _currentStamina = 0;
    public float CurrentStamina{
        get{return _currentStamina;}
        set{
            // MOVED FROM PLAYER MANAGER
            // ResetStaminaRegenTimer(_currentStamina, value);
            // UPDATES THE TOTAL AMOUNT OF HEALTH OR STAMINA WHEN MAX STAT AMOUNT CHANGES
            // SetNewMaxStaminaValue(maxStamina, value);

            // UPDATES STAMINA UI BAR WHEN STAMINA CHANGES 
            PlayerUIManager.instance.playerUIHudManager.SetNewStaminaValue(_currentStamina, value);
            _currentStamina = value;
        }
    }

    [Header("CHARACTER HEALTH STATS")]
    public int vitality = 1;
    public int maxHealth = 0;

    private float _currentHealth = 0;
    public float CurrentHealth{
        get{return _currentHealth;}
        set{
            // UPDATES THE TOTAL AMOUNT OF HEALTH OR STAMINA WHEN MAX STAT AMOUNT CHANGES
            // SetNewMaxHealthValue(maxHealth, value);

            // UPDATES HEALTH UI BAR WHEN HEALTH CHANGES 
            PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue(_currentHealth, value);
            _currentHealth = value;
            Debug.Log(_currentHealth);
            // CalculateHealthBasedOnVitalityLevel();
        }
    }

    protected virtual void Awake(){
        character = GetComponent<CharacterManager>();
    }

    protected virtual void Start()
    {
        //
    }

    public int CalculateHealthBasedOnVitalityLevel(int vitality){
        float health = 0;

        health = vitality * 15;

        return Mathf.RoundToInt(health);
    }

    public int CalculateStaminaBasedOnEnduranceLevel(int endurance){
        float stamina = 0;

        stamina = endurance * 10;

        return Mathf.RoundToInt(stamina);
    }

     public virtual void RegenerateStamina(){

        // WE DO NOT WANT TO REGENERATE STAMINA IF WE ARE SPRINTING OR PERFORMING ACTION
        if(character.isSprinting || character.isPerformingAction)
            return;

        staminaRegenerationTimer += Time.deltaTime;

        if(staminaRegenerationTimer >= staminaRegenerationDelay){
            if(CurrentStamina < maxStamina){
                staminaTickTimer += Time.deltaTime;

                if(staminaTickTimer >= 0.1f){
                    staminaTickTimer = 0;
                    CurrentStamina += staminaRegenerationAmount;
                }
            }
        }
    }

    public virtual void ResetStaminaRegenTimer(float previousStaminaAmount, float currentStaminaAmount){
        // WE ONLY WANT TO RESET THE REGENERATION IF THE ACTION USED STAMINA
        // WE DON'T WANT TO RESET THE REGENERATION IF WE ARE ALREADY REGENERATING STAMINA
        if(currentStaminaAmount < previousStaminaAmount)
        {
            staminaRegenerationTimer = 0;
        }
    }
}
