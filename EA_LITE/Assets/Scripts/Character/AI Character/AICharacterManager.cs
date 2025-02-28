using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterManager : CharacterManager
{
    [Header("Character Name")]
    public string characterName = "";
    
    [HideInInspector] public AICharacterCombatManager aiCharacterCombatManager;
    [HideInInspector] public AICharacterLocomotionManager aiCharacterLocomotionManager;

    [Header("Navmesh Agent")]
    public NavMeshAgent navMeshAgent;

    [Header("Current State")]
    [SerializeField] protected AIState currentState;

    [Header("States")]
    [SerializeField] public IdleState idle;
    [SerializeField] public PursueTargetState pursueTarget;
    public CombatStanceState combatStance;
    public AttackState attack;
    public SpitAttackState spitAttack;
    public RangedAttackState rangedAttack;

    [Header("Shooting")]
    public Transform shootPoint;
    public bool isShooting = false;
    public LayerMask layerMask;
    public GameObject rockBullet;
    public Vector3 spread = new Vector3(0.06f, 0.06f, 0.06f);
    public float bulletTravelTime = 0.2f;

    protected override void Awake()
    {
        base.Awake();

        aiCharacterCombatManager = GetComponent<AICharacterCombatManager>();
        aiCharacterLocomotionManager = GetComponent<AICharacterLocomotionManager>();

        navMeshAgent = GetComponentInChildren<NavMeshAgent>();

        // use a copy of the scriptable object. so the originals are not modified
        idle = Instantiate(idle);
        pursueTarget = Instantiate(pursueTarget);

        currentState = idle;
    }

    protected override void Update()
    {
        base.Update();
        
        // recovery timer
        aiCharacterCombatManager.HandleActionRecovery(this);
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

        // reset enemy navMesh agent transform and rotation, done after state machine processes each tick
        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;

        if(aiCharacterCombatManager.currentTarget != null)
        {
            // target direction is current target position - the position of the chasing character
            aiCharacterCombatManager.targetsDirection = aiCharacterCombatManager.currentTarget.transform.position - transform.position;
            aiCharacterCombatManager.viewableAngle = WorldUtilityManager.instance.GetAngleOfTarget(transform, aiCharacterCombatManager.targetsDirection);
            //distance between aiCharacter and currentTarget
            aiCharacterCombatManager.distanceFromTarget = Vector3.Distance(transform.position, aiCharacterCombatManager.currentTarget.transform.position);
        }

        if(navMeshAgent.enabled)
        {
            Vector3 agentDestination = navMeshAgent.destination;
            float remainingDistance = Vector3.Distance(agentDestination, transform.position);

            // change isMoviable bool based on whether agent is within stopping distance (distance assigned in inspector)
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

    public IEnumerator IsShootingTimer()
    {
        isShooting = true;

        if(isShooting == true)
        {
            // Shoot();
            StartCoroutine(ShootBurst());
        }

        yield return new WaitForSeconds(3);
        isShooting = false;
    }

    private IEnumerator ShootBurst()
    {
        if(isShooting == true)
        {
            // IsMoving = false;
            yield return new WaitForSeconds(0.7f);
            while(isShooting == true)
            {
                // Debug.Log("SHOOT AT PLAYER: " + aiCharacterCombatManager.currentTarget);
                Shoot();
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    public void Shoot()
    {
        Vector3 direction = GetDirection();
        if(Physics.Raycast(shootPoint.position, aiCharacterCombatManager.currentTarget.transform.position, out RaycastHit hit, float.MaxValue, layerMask))
        {
            Debug.DrawLine(shootPoint.position, aiCharacterCombatManager.currentTarget.transform.position, Color.red, 1f);
        }

        GameObject rock = Instantiate(rockBullet, shootPoint.position, Quaternion.identity);

        StartCoroutine(SpawnRock(rock, hit));
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = aiCharacterCombatManager.currentTarget.transform.position;
        direction += new Vector3(Random.Range(-spread.x, spread.x), Random.Range(-spread.y, spread.y), Random.Range(-spread.z, spread.z));
        // direction.Normalize();
        // Debug.Log(direction);
        return direction;
    }

    // private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    // {
    //     float time = 0f;
    //     Vector3 startPosition = trail.transform.position;

    //     while(time < 1f)
    //     {
    //         trail.transform.position = Vector3.Lerp(startPosition, aiCharacterCombatManager.currentTarget.transform.position, time);
    //         time += Time.deltaTime / trail.time;

    //         yield return null;
    //     }

    //     trail.transform.position = aiCharacterCombatManager.currentTarget.transform.position;

    //     Destroy(trail.gameObject, trail.time);

    // }

    private IEnumerator SpawnRock(GameObject rock, RaycastHit hit)
    {
        Vector3 direction = GetDirection();
        float time = 0f;
        Vector3 rStartPosition = rock.transform.position;
        // Vector3 tStartPosition = trail.transform.position;

        while(time < 1f && rock != null)
        {
            rock.transform.position = Vector3.Lerp(rStartPosition, aiCharacterCombatManager.currentTarget.transform.position, time);
            time += Time.deltaTime / bulletTravelTime;

            yield return null;
        }

        if(rock != null)
        {
            rock.transform.position = aiCharacterCombatManager.currentTarget.transform.position;
            Destroy(rock.gameObject, bulletTravelTime);
        }
    }
}