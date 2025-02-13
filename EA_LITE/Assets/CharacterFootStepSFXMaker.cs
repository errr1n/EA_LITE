using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFootStepSFXMaker : MonoBehaviour
{
    CharacterManager character;

    AudioSource audioSource;
    GameObject steppedOnObject;

    private bool hasTouchedGround = false;
    private bool hasPlayedFootStepSFX = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        character = GetComponentInParent<CharacterManager>();
    }

    private void FixedUpdate()
    {
        CheckForFootSteps();
    }

    private void CheckForFootSteps()
    {
        if(character == null)
        {
            return;
        }

        if(!character.IsMoving)
        {
            return;
        }

        RaycastHit hit;

        if(Physics.Raycast(transform.position, character.transform.TransformDirection(Vector3.down), out hit, 0.05f, WorldUtilityManager.instance.GetEnviroLayers()))
        {
            hasTouchedGround = true;

            if(!hasPlayedFootStepSFX)
            {
                steppedOnObject = hit.transform.gameObject;
            }
        }
        else
        {
            hasTouchedGround = false;
            hasPlayedFootStepSFX = false;
            steppedOnObject = null;
        }

        if(hasTouchedGround && !hasPlayedFootStepSFX)
        {
            hasPlayedFootStepSFX = true;
            PlayFootStepSFX();
        }
    }

    private void PlayFootStepSFX()
    {
        character.characterSoundFXManager.PlayFootstepSoundFX();
    }
}
