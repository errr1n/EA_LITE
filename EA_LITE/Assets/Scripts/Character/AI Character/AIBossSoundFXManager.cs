using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossSoundFXManager : CharacterSoundFXManager
{
    [Header("Hand Whooshes")]
    public AudioClip[] handWhooshes;

    [Header("Stomp Impacts")]
    public AudioClip[] stompImpacts;

    public virtual void PlayStompImpactSoundFX()
    {
        if(stompImpacts.Length > 0)
        {
            PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(stompImpacts));
        }
    }
    
}
