using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantCharacterEffect : ScriptableObject
{
    [Header("Effect ID")]
    public int instantEffectID;

    protected virtual void Awake()
    {
        // characterStatsManager = GetComponent<CharacterStatsManager>();
    }

    public virtual void ProcessEffect(CharacterStatsManager characterStatsManager)
    {
        //
    }
}
