using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager character;

    int vertical;
    int horizontal;

    [Header("Damage Animations")]
    public string hit_Forward_Medium = "hit_Forward_Medium";
    public string hit_Backward_Medium = "hit_Backward_Medium";
    public string hit_Left_Medium = "hit_Left_Medium";
    public string hit_Right_Medium = "hit_Right_Medium";

    [Header("Flags")]
    public bool applyRootMotion = false;

    // list to randomize animations

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();

        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    //get random animations

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
        this.applyRootMotion = applyRootMotion;
        character.animator.CrossFade(targetAnimation, 0.2f);
        // CAN BE USED TO STOP CHARACTER FROM ATTEMPTING NEW ACTION
        // EX. DAMAGE STUN LOCK
        character.isPerformingAction = isPerformingAction;
        character.characterLocomotionManager.canRotate = canRotate;
        character.characterLocomotionManager.canMove = canMove;
    }

    // ADD ATTACK TYPE?
    public virtual void PlayTargetAttackActionAnimation(AttackType attackType, 
        string targetAnimation, 
        bool isPerformingAction, 
        bool applyRootMotion = true, 
        bool canRotate = false, 
        bool canMove = false)
    {
        // keep track of last attack performed for combos
        // keep track of current attack type (light, heavy)
        character.characterCombatManager.currentAttackType = attackType; // May not need
        // update animation set to current weapon animations
        this.applyRootMotion = applyRootMotion;
        character.animator.CrossFade(targetAnimation, 0.2f);
        // CAN BE USED TO STOP CHARACTER FROM ATTEMPTING NEW ACTION
        // EX. DAMAGE STUN LOCK
        character.isPerformingAction = isPerformingAction;
        character.characterLocomotionManager.canRotate = canRotate;
        character.characterLocomotionManager.canMove = canMove;
    }


}
