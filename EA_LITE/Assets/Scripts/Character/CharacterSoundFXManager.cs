using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundFXManager : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Damage Grunts")]
    [SerializeField] protected AudioClip[] damageGrunts;

    [Header("Attack Grunts")]
    [SerializeField] protected AudioClip[] attackGrunts;

    [Header("Footsteps")]
    [SerializeField] protected AudioClip[] footSteps;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //not used yet
    public void PlaySoundFX(AudioClip soundFX, float volume = 1, bool randomizePitch = true, float pitchRandom = 0.1f)
    {
        audioSource.PlayOneShot(soundFX, volume);
        //reset pitch
        audioSource.pitch = 1;

        if(randomizePitch)
        {
            audioSource.pitch += Random.Range(-pitchRandom, pitchRandom);
        }

    }

    public void PlayRollSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.rollSFX);
    }

    public void PlayBackStepSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundFXManager.instance.backStepSFX);
    }

    public virtual void PlayDamageGrunt()
    {
        PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(damageGrunts));
    }

    public virtual void PlayAttackGrunt()
    {
        if(attackGrunts.Length > 0)
        {
            PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(attackGrunts));
        }
    }

    public virtual void PlayFootstepSoundFX()
    {
        if(footSteps.Length > 0)
        {
            PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(footSteps));
        }
    }
}
