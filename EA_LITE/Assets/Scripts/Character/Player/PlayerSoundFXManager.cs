using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundFXManager : CharacterSoundFXManager
{
    [Header("ACTION SFX")]
    public AudioClip rollSFX;
    public AudioClip backStepSFX;

    public void PlayRollSoundFX()
    {
        audioSource.PlayOneShot(rollSFX);
    }

    public void PlayBackStepSoundFX()
    {
        audioSource.PlayOneShot(backStepSFX);
    }
}
