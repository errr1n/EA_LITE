using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager character;
    // PlayerLocomotionManager playerLoco;
    // PlayerManager player;

    int vertical;
    int horizontal;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
        // player = GetComponent<PlayerManager>();

        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    public void UpdateAnimatorMovementParameters(float horizontalMovement, float verticalMovement)
    {
        float horizontalAmount = horizontalMovement;
        float verticalAmount = verticalMovement;

        if(character.isSprinting)
        {
            verticalAmount = 2;
        }

        // ACCESSING HUMANOID ANIMATOR PARAMETERS "HORIZONTAL" AND "VERTICAL" (WALK AND RUN ANIMATIONS)
        character.animator.SetFloat(horizontal, horizontalAmount, 0.1f, Time.deltaTime);
        character.animator.SetFloat(vertical, verticalAmount, 0.1f, Time.deltaTime);

        // IF ANIMATIONS DON'T LOOK SMOOTH, MAY NEED TO CLAMP HERE
    }

    public virtual void PlayTargetActionAnimation(string targetAnimation, 
        bool isPerformingAction, 
        bool applyRootMotion = true, 
        bool canRotate = false, 
        bool canMove = false)
    {
        character.applyRootMotion = applyRootMotion;
        character.animator.CrossFade(targetAnimation, 0.2f);
        // CAN BE USED TO STOP CHARACTER FROM ATTEMPTING NEW ACTION
        // EX. DAMAGE STUN LOCK
        character.isPerformingAction = isPerformingAction;
        character.canRotate = canRotate;
        character.canMove = canMove;
    }
}
