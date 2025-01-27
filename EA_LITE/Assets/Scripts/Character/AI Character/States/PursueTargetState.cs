using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "A.I/States/Pursue Target")]

public class PursueTargetState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacter)
    {
        // Debug.Log("0");
        // return base.Tick(aiCharacter);

        // CHECK IF WE'RE PERFORMING AN ACTION (DO NOT MOVE)
        if(aiCharacter.isPerformingAction)
        {
            return this;
        }

        // Debug.Log("1");
        // CHECK IF TARGET IS NULL, IF WE DO NOT HAVE A TARGET RETURN TO IDLE
        if(aiCharacter.aiCharacterCombatManager.currentTarget == null)
        {
            return SwitchState(aiCharacter, aiCharacter.idle);
        }

        // Debug.Log("2");
        // MAKE SURE NAV MESH AGENT IS ACTIVE, IF NOT ENABLE
        if(!aiCharacter.navMeshAgent.enabled)
        {
            aiCharacter.navMeshAgent.enabled = true;
        }

        // Debug.Log("3");

        // if our target is outside of fov, pivot to face them
        if(aiCharacter.aiCharacterCombatManager.viewableAngle < aiCharacter.aiCharacterCombatManager.minimumFOV || aiCharacter.aiCharacterCombatManager.viewableAngle > aiCharacter.aiCharacterCombatManager.maximumFOV)
        {
            aiCharacter.aiCharacterCombatManager.PivotTowardsTarget(aiCharacter);
            // Debug.Log("4");
        }

        aiCharacter.aiCharacterLocomotionManager.RotateTowardsAgent(aiCharacter);
        // Debug.Log("5");

        // IF WITHIN COMBAT RANGE, SWITCH TO COMBAT STATE
        if(aiCharacter.aiCharacterCombatManager.distanceFromTarget <= aiCharacter.navMeshAgent.stoppingDistance)
        {
            // Debug.Log("6");
            return SwitchState(aiCharacter, aiCharacter.combatStance);
        }

        // Debug.Log("7");

        // IF THE TARGET IS NOT REACHABLE AND FAR AWAY, RETURN HOME

        // PURSUE THE TARGET
        NavMeshPath path = new NavMeshPath();
        aiCharacter.navMeshAgent.CalculatePath(aiCharacter.aiCharacterCombatManager.currentTarget.transform.position, path);
        aiCharacter.navMeshAgent.SetPath(path);

        // Debug.Log("8");

        return this;
    }
}
