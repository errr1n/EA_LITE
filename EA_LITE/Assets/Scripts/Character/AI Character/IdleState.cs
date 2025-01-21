using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/States/Idle")]

public class IdleState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacter)
    {
        return base.Tick(aiCharacter);

        // if(aiCharacter.characterCombatManager.currentTarget != null)
        // {
        //     // return the pursue target state
        //     Debug.log("WE HAVE A TARGET");
        // }
        // else
        // {
        //     //return this state, to continually search for a target
        //     Debug.log("WE HAVE NO TARGET");
        // }

        return this;
    }
}
