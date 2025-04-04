using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossSoundFXManager : CharacterSoundFXManager
{
    [Header("Hand Whooshes")]
    public AudioClip[] handWhooshes;

    [Header("Stomp Impacts")]
    public AudioClip[] stompImpacts;

    [Header("Spit Sounds")]
    public AudioClip[] spitSounds;
    
    [Header("Death Sounds")]
    public AudioClip[] deathSounds;


    // [Header("Footsteps")]
    // public AudioClip[] footSteps;

    public virtual void PlayStompImpactSoundFX()
    {
        // if there is a sound in the stomp impact array 
        if(stompImpacts.Length > 0)
        {
            // play the stomp impact sound
            PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(stompImpacts));
        }
    }

    public virtual void PlayDeathSoundFX()
    {
        // if there is a sound in the stomp impact array 
        if(deathSounds.Length > 0)
        {
            // play the stomp impact sound
            PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(deathSounds));
        }
    }



    // public virtual void PlayFootstepSoundFX()
    // {
    //     // if there is a sound in the stomp impact array 
    //     if(footSteps.Length > 0)
    //     {
    //         // play the stomp impact sound
    //         PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(footSteps));
    //     }
    // }
    
}
