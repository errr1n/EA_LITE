using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Stamina Damage")]

public class TakeStaminaDamageEffect : InstantCharacterEffect
{
    // [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Stamina Damage")]
    public float staminaDamage;

    // MIGHT NEED TO CHANGE TO CharacterStatsManager characterStatsManager
    public override void ProcessEffect(CharacterStatsManager characterStatsManager)
    {
        // WHY DO WE DO THIS
        // base.ProcessEffect(character);

        CalculateStaminaDamage(characterStatsManager);
    }

    private void CalculateStaminaDamage(CharacterStatsManager characterStatsManager)
    {
        // COMPARE THE BASE STAMINA DAMAGE AGAISNT OTHER PLAYER EFFECTS/MODIFIERS
        // CHANGE THE VALUE BEFORE SUBTRACTING/ADDING IT
        // PLAY SOUND FX OR VFX DURING THE EFFECT

        // Debug.Log("CHARACTER IS TAKING: " + staminaDamage + " STAMINE DAMAGE");
        characterStatsManager.CurrentStamina -= staminaDamage; 
    }
}
