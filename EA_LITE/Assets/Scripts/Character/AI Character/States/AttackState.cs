using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/States/Attack")]

public class AttackState : AIState
{
    [Header("Current Attack")]
    // get current attack action (stomp, swat)
    [HideInInspector] public AICharacterAttackAction currentAttack;
    // will perform combo false by default
    [HideInInspector] public bool willPerformCombo = false;

    [Header("State Flags")]
    [SerializeField] protected bool hasPerformedAttack = false;
    protected bool hasPerformedCombo = false;

    [Header("Pivot After Attack")]
    // pivot, or just turn organically
    [SerializeField] protected bool pivotAfterAttack = false;

    public override AIState Tick(AICharacterManager aiCharacter)
    {
        // if target is null, return to idle
        if(aiCharacter.aiCharacterCombatManager.currentTarget == null)
        {
            return SwitchState(aiCharacter, aiCharacter.idle);
        }

        // if target is dead, return to idle
        if(aiCharacter.aiCharacterCombatManager.currentTarget.isDead)
        {
            return SwitchState(aiCharacter, aiCharacter.idle);
        }

        //rotate towards the target while attacking
        aiCharacter.aiCharacterCombatManager.RotateTowardsTargetWhileAttacking(aiCharacter);

        //set movement to 0
        aiCharacter.characterAnimatorManager.UpdateAnimatorMovementParameters(0, 0);

        // perform a combo
        if(willPerformCombo && !hasPerformedCombo)
        {
            if(currentAttack.comboAction != null)
            {
                // if can combo
                // hasPerformedAttack = true;
                // currentAttack.comboAction.AttemptToPerformAction(aiCharacter);
            }
        }

        // if already performing action, return
        if(aiCharacter.isPerformingAction)
        {
            return this;
        }

        if(!hasPerformedAttack)
        {
            // if we are still recovering from an action, wait before performing another 
            if(aiCharacter.aiCharacterCombatManager.actionRecoveryTimer > 0)
            {
                return this;
            }

            PerformAttack(aiCharacter);

            // return to the top, so if we have a combo we process that when we are able
            return this;
        }

        //if character is allowed to pivot
        if(pivotAfterAttack)
        {
            aiCharacter.aiCharacterCombatManager.PivotTowardsTarget(aiCharacter);
        }

        //return to combat stance
        return SwitchState(aiCharacter, aiCharacter.combatStance);
    }

    protected void PerformAttack(AICharacterManager aiCharacter)
    {
        hasPerformedAttack = true;
        // have this character attempt to perform an attack action
        currentAttack.AttemptToPerformAction(aiCharacter);
        // set action recovery timer
        aiCharacter.aiCharacterCombatManager.actionRecoveryTimer = currentAttack.actionRecoveryTime;
    }

    protected override void ResetStateFlags(AICharacterManager aiCharacter)
    {
        base.ResetStateFlags(aiCharacter);

        hasPerformedAttack = false;
        hasPerformedCombo = false;
    }
}
