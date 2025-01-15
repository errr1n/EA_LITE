using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    CharacterManager character;

    public int endurance = 1;

    public int maxStamina = 0;

    [Header("STAMINA REGENERATION")]
    private float staminaRegenerationTimer = 0;
    private float staminaTickTimer = 0;
    [SerializeField] float staminaRegenerationDelay = 2;
    [SerializeField] float staminaRegenerationAmount = 2;

    private float _currentStamina = 0;
    public float CurrentStamina{
        get{return _currentStamina;}
        set{
            ResetStaminaRegenTimer(_currentStamina, value);
            PlayerUIManager.instance.playerUIHudManager.SetNewStaminaValue(_currentStamina, value);
            _currentStamina = value;
        }
    }

    protected virtual void Awake(){
        character = GetComponent<CharacterManager>();
    }

    //was int
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
