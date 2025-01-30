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

    public override void ProcessEffect(CharacterManager character)
    {
        base.ProcessEffect(character);

        // IF CHARACTER IS DEAD DO NOT PROCESS EFFECTS
        if(character.isDead)
        {
            return;
        }

            // CHECK FOR INVULNERABILITY (DODGING)

            // CALCULATE DAMAGE
            CalculateDamage(character);
            // CHECK WHICH DIRECTION THE DAMAGE CAME FROM
            //PLAY A DAMAGE ANIMATION
            // CHECK BUILD UPS?
            // PLAY DAMAGE SOUND FX
            PlayDamageSFX(character);
            // PLAY DAMAGE VFX? (BLOOD)
    }

    private void CalculateDamage(CharacterManager character)
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

        character.characterStatsManager.CurrentHealth -= finalDamageDealt;
    }

    //play damage vfx

    //player damage sfx
    private void PlayDamageSFX(CharacterManager character)
    {
        // AudioClip physicalDamageSFX = WorldSoundFXManager.instance.ChooseRandomSFXFromArray(WorldSoundFXManager.instance.physicalDamageSFX);

        // character.characterSoundFXManager.PlaySoundFX(physicalDamageSFX);

        character.characterSoundFXManager.PlayDamageGrunt();
    }

    // NOT USED
    private void PlayDirectionalBasedDamageAnimation(CharacterManager character)
    {
        if(character.isDead)
        {
            // Debug.Log("DEAD");
            return;
        }
        
        if(angleHitFrom >= 145 && angleHitFrom <= 180)
        {
            //front animation
            character.characterAnimatorManager.PlayTargetActionAnimation(character.characterAnimatorManager.hit_Forward_Medium, true);
        }
        else if(angleHitFrom <= -145 && angleHitFrom >= -180)
        {
            //front animation
            character.characterAnimatorManager.PlayTargetActionAnimation(character.characterAnimatorManager.hit_Forward_Medium, true);
        }
        else if(angleHitFrom >= -45 && angleHitFrom <= 45)
        {
            //play back
            character.characterAnimatorManager.PlayTargetActionAnimation(character.characterAnimatorManager.hit_Backward_Medium, true);
        }
        else if(angleHitFrom >= -144 && angleHitFrom <= -45)
        {
            //play left
            character.characterAnimatorManager.PlayTargetActionAnimation(character.characterAnimatorManager.hit_Left_Medium, true);
        }
        else if(angleHitFrom >= 45 && angleHitFrom <= 144)
        {
            //play right
            character.characterAnimatorManager.PlayTargetActionAnimation(character.characterAnimatorManager.hit_Right_Medium, true);
        }
    }
}
