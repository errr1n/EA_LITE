using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "A.I/States/Pursue Target")]

public class PursueTargetState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacter)
    {
        // Debug.Log("PURSUE TARGET");
        // return base.Tick(aiCharacter);

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
        if(aiCharacter.aiCharacterCombatManager.viewableAngle < aiCharacter.aiCharacterCombatManager.minimumFOV || aiCharacter.aiCharacterCombatManager.viewableAngle > aiCharacter.aiCharacterCombatManager.MaximumFOV)
        {
            aiCharacter.aiCharacterCombatManager.PivotTowardsTarget(aiCharacter);
        }

        aiCharacter.aiCharacterLocomotionManager.RotateTowardsAgent(aiCharacter);

        // IF WITHIN COMBAT RANGE, SWITCH TO COMBAT STATE
        if(aiCharacter.aiCharacterCombatManager.distanceFromTarget <= aiCharacter.navMeshAgent.stoppingDistance)
        {
            return SwitchState(aiCharacter, aiCharacter.combatStance);
        }

        // IF THE TARGET IS NOT REACHABLE AND FAR AWAY, RETURN HOME

        // PURSUE THE TARGET
        NavMeshPath path = new NavMeshPath();
        aiCharacter.navMeshAgent.CalculatePath(aiCharacter.aiCharacterCombatManager.currentTarget.transform.position, path);
        aiCharacter.navMeshAgent.SetPath(path);

        return this;
    }
}
