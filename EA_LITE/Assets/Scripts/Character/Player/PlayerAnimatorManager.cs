using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : CharacterAnimatorManager
{
    CharacterManager character;

    protected override void Awake()
    {
        base.Awake();

        character = GetComponent<CharacterManager>();
    }

    private void OnAnimatorMove()
    {
        if(character.applyRootMotion)
        {
            // TAKE POSTION AND ROTATION FROM ANIMATION AND APPLY TO CHARACTER
            Vector3 velocity = character.animator.deltaPosition;
            character.characterController.Move(velocity);
            character.transform.rotation *= character.animator.deltaRotation;
        }
    }
}
