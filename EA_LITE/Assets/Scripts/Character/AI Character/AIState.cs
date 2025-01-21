using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : ScriptableObject
{
    public virtual AIState Tick(AICharacterManager aiCharacter)
    {
        // Debug.Log("WE ARE RUNNING THIS STATE");
        //DO SOME LOGIC TO FIND THE PLAYER

        //IF WE HAVE FOUN THE PLAYER, RETURN THE PURSUE STATE

        //IF WE HAVE NOT FOUND THE PLAYER, CONTINUE TO RETURN THE IDLE STATE
        return this;
    }
}
