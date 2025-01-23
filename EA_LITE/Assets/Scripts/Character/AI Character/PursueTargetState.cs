using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueTargetState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacter)
    {
        return base.Tick(aiCharacter);

        // CHECK IF WE'RE PERFORMING AN ACTION (DO NOT MOVE)

        // CHECK IF TARGET IS NULL, IF WE DO NOT HAVE A TARGET RETURN TO IDLE

        // MAKE SURE NAV MESH AGENT IS ACTIVE, IF NOT ENABLE

        // IF WITHIN COMBAT RANGE, SWITCH TO COMBAT STATE

        // IF THE TARGET IS NOT REACHABLE AND FAR AWAY, RETURN HOME

        // PURSUE THE TARGET
    }
}
