using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterManager : CharacterManager
{
    [HideInInspector] public AICharacterCombatManager aiCharacterCombatManager;
    // characterStatsManager characterStatsManager;

    [Header("Navmesh Agent")]
    public NavMeshAgent navMeshAgent;

    [Header("Current State")]
    [SerializeField] AIState currentState;

    [Header("States")]
    [SerializeField] public IdleState idle;
    [SerializeField] public PursueTargetState pursueTarget;

    // [SerializeField] public bool isMoving = false;

    protected override void Awake()
    {
        base.Awake();

        aiCharacterCombatManager = GetComponent<AICharacterCombatManager>();
        // characterStatsManager = GetComponent<characterStatsManager>();

        navMeshAgent = GetComponentInChildren<NavMeshAgent>();

        // use a copy of the scriptable object. so the originals are not modified
        idle = Instantiate(idle);
        pursueTarget = Instantiate(pursueTarget);

        currentState = idle;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        ProcessStateMachine();
    }

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

        if(navMeshAgent.enabled)
        {
            Vector3 agentDestination = navMeshAgent.destination;
            float remainingDistance = Vector3.Distance(agentDestination, transform.position);

            if(remainingDistance > navMeshAgent.stoppingDistance)
            {
                IsMoving = true;
            }
            else
            {
                IsMoving = false;
            }
        }
        else
        {
            IsMoving = false;
        }
    }
}