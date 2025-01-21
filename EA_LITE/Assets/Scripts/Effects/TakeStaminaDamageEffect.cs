using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Stamina Damage")]

public class TakeStaminaDamageEffect : InstantCharacterEffect
{
    // [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Stamina Damage")]
    public float staminaDamage;

    // MIGHT NEED TO CHANGE TO CharacterStatsManager characterStatsManager
    public override void ProcessEffect(CharacterManager characterManager)
    {
        // WHY DO WE DO THIS
        // base.ProcessEffect(character);

        CalculateStaminaDamage(characterManager);
    }

    private void CalculateStaminaDamage(CharacterManager characterManager)
    {
        // COMPARE THE BASE STAMINA DAMAGE AGAISNT OTHER PLAYER EFFECTS/MODIFIERS
        // CHANGE THE VALUE BEFORE SUBTRACTING/ADDING IT
        // PLAY SOUND FX OR VFX DURING THE EFFECT

        // Debug.Log("CHARACTER IS TAKING: " + staminaDamage + " STAMINE DAMAGE");
        characterManager.characterStatsManager.CurrentStamina -= staminaDamage; 
    }
}
