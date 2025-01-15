using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    // public void CalculateStaminaBasedOnEnduranceLevel(int endurance)
    // {
    //     float stamina = 0;

    //     // CREATE AN EQUATION FOR HOW YOU WANT YOUR STAMINA TO BE CALCULATED
    //     stamina = endurance * 10;
    // }

    CharacterManager character;
    PlayerManager player;

    public int endurance = 1;

    public int maxStamina = 0;
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
        player = GetComponent<PlayerManager>();
    }

    //was int
    public int CalculateStaminaBasedOnEnduranceLevel(int endurance){
        float stamina = 0;

        stamina = endurance * 10;

        return Mathf.RoundToInt(stamina);
    }

     public virtual void RegenerateStamina(){
        if(player.isSprinting || character.isPerformingAction)
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

    public virtual void ResetStaminaRegenTimer(float oldValue, float newValue){
        if(newValue < oldValue){
            staminaRegenerationTimer = 0;
        }
    }
}
