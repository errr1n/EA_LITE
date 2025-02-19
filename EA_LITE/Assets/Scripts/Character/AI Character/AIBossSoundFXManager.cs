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
        // if there is a sound in the stomp impact array 
        if(stompImpacts.Length > 0)
        {
            // play the stomp impact sound
            PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(stompImpacts));
        }
    }
    
}
