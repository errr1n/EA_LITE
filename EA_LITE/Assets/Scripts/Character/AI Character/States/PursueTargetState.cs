using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "A.I/States/Pursue Target")]

public class PursueTargetState : AIState
{
    private float spitTimer = 5;
    // private bool canUseSpitAttack = false;


    public override AIState Tick(AICharacterManager aiCharacter)
    {
        if(spitTimer > 0)
        {
            spitTimer -= Time.deltaTime;
        }
        else
        {
            spitTimer = 0;
        }

        // Debug.Log(spitTimer);

        // CHECK IF WE'RE PERFORMING AN ACTION (DO NOT MOVE)
        if(aiCharacter.isPerformingAction)
        {
            return this;
        }

        // CHECK IF TARGET IS NULL, IF WE DO NOT HAVE A TARGET RETURN TO IDLE
        if(aiCharacter.aiCharacterCombatManager.currentTarget == null)
        {
            return SwitchState(aiCharacter, aiCharacter.idle);
        }

        // MAKE SURE NAV MESH AGENT IS ACTIVE, IF NOT ENABLE
        if(!aiCharacter.navMeshAgent.enabled)
        {
            aiCharacter.navMeshAgent.enabled = true;
        }

        // if our target is outside of fov, pivot to face them
        if(aiCharacter.aiCharacterCombatManager.enablePivot)
        {
            if(aiCharacter.aiCharacterCombatManager.viewableAngle < aiCharacter.aiCharacterCombatManager.minimumFOV || aiCharacter.aiCharacterCombatManager.viewableAngle > aiCharacter.aiCharacterCombatManager.maximumFOV)
            {
                aiCharacter.aiCharacterCombatManager.PivotTowardsTarget(aiCharacter);
            }
        }

        aiCharacter.aiCharacterLocomotionManager.RotateTowardsAgent(aiCharacter);

        // IF WITHIN COMBAT RANGE, SWITCH TO COMBAT STATE
        if(aiCharacter.aiCharacterCombatManager.distanceFromTarget <= aiCharacter.navMeshAgent.stoppingDistance)
        {
            return SwitchState(aiCharacter, aiCharacter.combatStance);
        }

        // IF THE TARGET IS NOT REACHABLE AND FAR AWAY, RETURN HOME

        // if we are in this state for longer than timer length
        if(spitTimer == 0)
        {
            spitTimer = 5;
            // switch to spit
            return SwitchState(aiCharacter, aiCharacter.spitAttack);
        }

        // PURSUE THE TARGET
        NavMeshPath path = new NavMeshPath();
        aiCharacter.navMeshAgent.CalculatePath(aiCharacter.aiCharacterCombatManager.currentTarget.transform.position, path);
        aiCharacter.navMeshAgent.SetPath(path);

        return this;
    }
}
