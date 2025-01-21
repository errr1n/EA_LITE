using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundFXManager : MonoBehaviour
{
    private AudioSource audioSource;

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
}
