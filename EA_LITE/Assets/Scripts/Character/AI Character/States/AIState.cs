using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : ScriptableObject
{
    public float spitTimer;

    public virtual AIState Tick(AICharacterManager aiCharacter)
    {
        //DO SOME LOGIC TO FIND THE PLAYER

        //IF WE HAVE FOUND THE PLAYER, RETURN THE PURSUE STATE

        //IF WE HAVE NOT FOUND THE PLAYER, CONTINUE TO RETURN THE IDLE STATE
        return this;
    }

    protected virtual AIState SwitchState(AICharacterManager aiCharacter, AIState newState)
    {
        ResetStateFlags(aiCharacter);
        return newState;
    }

    protected virtual void ResetStateFlags(AICharacterManager aiCharacter)
    {
        //reset any state flags here so when you return the state. they are blank once again
    }
}
