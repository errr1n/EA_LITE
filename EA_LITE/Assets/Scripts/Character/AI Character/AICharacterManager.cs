using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterManager : CharacterManager
{
    [Header("Current State")]
    [SerializeField] AIState currentState;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        ProcessStateMachine();
    }


    //OPTION 1
    private void ProcessStateMachine()
    {
        AIState nextState = null;

        if(currentState != null)
        {
            nextState = currentState.Tick(this);
        }

        if(nextState != null)
        {
            currentState = nextState;
        }
    }

    //OPTION 2
    // private void ProcessStateMachine2()
    // {
    //     AIState nextState = currentState?.Tick(this);

    //     if(nextState != null)
    //     {
    //         currentState = nextState;
    //     }
    // }
}
