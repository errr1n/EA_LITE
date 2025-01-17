using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Damage")]

public class TakeDamageEffect : InstantCharacterEffect
{
    [Header("Character Causing Damage")]
    public CharacterManager characterCausingDamage; // IF DAMAGE CAUSED BY ANOTHER CHARACTER

    [Header("Damage")]
    public float physicalDamage = 0; // WILL BE BROKEN INTO SUBTYPES STANDARD, STRIKE, SLASH, PIERCE
    
    [Header("Final Damage")]
    private int finalDamageDealt = 0; // the damage the character takes after ALL calculations have been made

    //can do build ups

    [Header("Animation")]
    public bool playDamageAnimation = true;
    public bool manuallySelectDamageAnimation = false;
    public string damageAnimation;

    [Header("Sound FX")]
    public bool willPlayDamageSFX = true;

    [Header("Direction Damage Taken From")]
    public float angleHitFrom; // USED TO DETERMINE WHAT DAMAGE ANIMATION TO PLAY
    public Vector3 contactPoint; // POINT ON COLLIDER WHERE DAMAGE IS TAKEN 

    public override void ProcessEffect(CharacterStatsManager characterStatsManager)
    {
        base.ProcessEffect(characterStatsManager);

        // IF CHARACTER IS DEAD DO NOT PROCESS EFFECTS
        if(characterStatsManager.isDead)
        {
            return;
        }

            // CHECK FOR INVULNERABILITY (DODGING)

            // CALCULATE DAMAGE
            CalculateDamage(characterStatsManager);
            // CHECK WHICH DIRECTION THE DAMAGE CAME FROM
            //PLAY A DAMAGE ANIMATION
            // CHECK BUILD UPS?
            // PLAY DAMAGE SOUND FX
            // PLAY DAMAGE VFX? (BLOOD)
    }

    private void CalculateDamage(CharacterStatsManager characterStatsManager)
    {
        // if()
        // {
        //     //
        // }

        if(characterCausingDamage != null)
        {
            // CHECK FOR DAMAGE MODIFIERS

        }

        // ADD ALL DAMAGE TYPES TOGETHER
        finalDamageDealt = Mathf.RoundToInt(physicalDamage);

        if(finalDamageDealt <= 0)
        {
            finalDamageDealt = 1;
        }

        characterStatsManager.CurrentHealth -= finalDamageDealt;
    }


}
