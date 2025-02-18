using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/States/Idle")]

public class IdleState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacter)
    {
        // if current target is not null
        if(aiCharacter.characterCombatManager.currentTarget != null)
        {
            // return the pursue target state
            return SwitchState(aiCharacter, aiCharacter.pursueTarget);
        }
        else
        {
            //return this state, to continually search for a target
            aiCharacter.aiCharacterCombatManager.FindATargetViaLineOfSight(aiCharacter);
            return this;
        }
    }
}