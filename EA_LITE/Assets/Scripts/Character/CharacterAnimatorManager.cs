using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager character;
    PlayerLocomotionManager playerLoco;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
        playerLoco = GetComponent<PlayerLocomotionManager>();

        // vertical = Animator
    }

    public void UpdateAnimatorMovementParameters(float horizontalMovement, float verticalMovement)
    {
        float horizontal = horizontalMovement;
        float vertical = verticalMovement;

        if(playerLoco.isSprinting)
        {
            vertical = 2;
        }

        // ACCESSING HUMANOID ANIMATOR PARAMETERS "HORIZONTAL" AND "VERTICAL" (WALK AND RUN ANIMATIONS)
        character.animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
        character.animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);

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
