using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/States/Ranged Attack")]

public class RangedAttackState : AttackState
{
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
            // Shoot();

            // return to the top, so if we have a combo we process that when we are able
            return this;
        }

        //if character is allowed to pivot
        if(pivotAfterAttack)
        {
            aiCharacter.aiCharacterCombatManager.PivotTowardsTarget(aiCharacter);
        }

        //return to pursue target
        return SwitchState(aiCharacter, aiCharacter.pursueTarget);
    }

    protected override void PerformAttack(AICharacterManager aiCharacter)
    {
        hasPerformedAttack = true;
        // have this character attempt to perform an attack action
        currentAttack.AttemptToPerformAction(aiCharacter);
        // Shoot();
        aiCharacter.StartCoroutine(aiCharacter.Shoot());
        // set action recovery timer
        aiCharacter.aiCharacterCombatManager.actionRecoveryTimer = currentAttack.actionRecoveryTime;
    }

    // private IEnumerator Shoot()
    // {
    //     Debug.Log("SHOOT");
    //     yield return new WaitForSeconds(3);
    //     Debug.Log("SHOOT OFF");
    // }
}
