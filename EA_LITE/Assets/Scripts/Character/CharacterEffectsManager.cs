using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectsManager : MonoBehaviour
{
    // PROCESS INSTANT EFFECTS (TAKING DAMAGE, HEALING)

    // PROCESS TIMED EFFECTS (POISON, BUILD UPS)

    // PROCESS STATIC EFFECTS (ADDING/ REMOVING BUFFS)

    // CharacterManager character;
    CharacterManager characterManager;

    // [Header("VFX")]
    //can do bloodsplatter or rocks of whatever particles

    protected virtual void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
    }

    public virtual void ProcessInstantEffect(InstantCharacterEffect effect)
    {
        // TAKE IN EFFECT
        // PROCESS IT
        effect.ProcessEffect(characterManager);
    }

    // play blood splatter or other particles
}
