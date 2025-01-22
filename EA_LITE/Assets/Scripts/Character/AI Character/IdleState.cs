using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/States/Idle")]

public class IdleState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacter)
    {
        // return base.Tick(aiCharacter);

        // Debug.Log(aiCharacter.characterCombatManager.currentTarget);

        if(aiCharacter.characterCombatManager.currentTarget != null)
        {
            // return the pursue target state
            Debug.Log("WE HAVE A TARGET");

            // return this;
        }
        else
        {
            //return this state, to continually search for a target
            // aiCharacter.aiCharacterCombatManager.FindATargetViaLineOfSight(aiCharacter);
            Debug.Log("SEARCHING FOR TARGET");

            // return this;
        }

        return this;
    }
}