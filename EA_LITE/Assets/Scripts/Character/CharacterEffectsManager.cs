using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectsManager : MonoBehaviour
{
    // PROCESS INSTANT EFFECTS (TAKING DAMAGE, HEALING)

    // PROCESS TIMED EFFECTS (POISON, BUILD UPS)

    // PROCESS STATIC EFFECTS (ADDING/ REMOVING BUFFS)

    // CharacterManager character;
    CharacterStatsManager characterStatsManager;

    protected virtual void Awake()
    {
        characterStatsManager = GetComponent<CharacterStatsManager>();
    }

    public virtual void ProcessInstantEffect(InstantCharacterEffect effect)
    {
        // TAKE IN EFFECT
        // PROCESS IT
        effect.ProcessEffect(characterStatsManager);
    }
}
