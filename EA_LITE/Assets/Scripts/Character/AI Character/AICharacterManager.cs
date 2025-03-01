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
    public Transform raycastStartPosition;
    public bool isShooting = false;
    public LayerMask layerMask;
    public GameObject rockBullet;
    public Vector3 spread = new Vector3(0.06f, 0.06f, 0.06f);
    public float bulletTravelTime = 0.2f;

    private GameObject shootPointPlayer;

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

    // length of time the boss is shooting for
    public IEnumerator IsShootingTimer()
    {
        isShooting = true;

        if(isShooting == true)
        {
            // Shoot();
            StartCoroutine(ShootingDelay());
        }

        yield return new WaitForSeconds(3);
        isShooting = false;
    }

    // time between each of the shots
    private IEnumerator ShootingDelay()
    {
        if(isShooting == true)
        {
            // wait for animation to open it's mouth
            yield return new WaitForSeconds(0.7f);
            while(isShooting == true)
            {
                // Shoot with a 0.2s pause between each projectile spawned
                Shoot();
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    public void Shoot()
    {
        shootPointPlayer = GameObject.Find("/Player/PlayerTargetPoint");
        
        if(Physics.Raycast(raycastStartPosition.position, transform.forward, out RaycastHit hit, layerMask))
        {
            Debug.DrawLine(raycastStartPosition.position, hit.point, Color.red, 1f);
        }

        // Debug.Log("shootPointPlayer: " + shootPointPlayer.transform.position);
        // Debug.Log("hitPoint: " + hit.point);
        // Debug.Log("hitPoint name: " + hit.collider.gameObject.name);

        GameObject rock = Instantiate(rockBullet, shootPoint.position, Quaternion.identity);

        StartCoroutine(MoveProjectile(rock, hit));
    }

    private IEnumerator MoveProjectile(GameObject rock, RaycastHit hit)
    {
        float time = 0f;
        Vector3 rStartPosition = rock.transform.position;
        
        Vector3 bulletLocation = hit.point;

        // Debug.Log(hit.collider.GetComponentInParent<PlayerManager>());
        if(hit.collider.GetComponentInParent<PlayerManager>())
        {

            bulletLocation = shootPointPlayer.transform.position;
            // bulletLocation = aiCharacterCombatManager.currentTarget.transform.position;
        }

        while(time < 1f)
        {
            // NEED THIS TO BE THE PLAYER POSITION, BUT NEED TO CHECK FIRST THAT THE HIT.POINT COLLIDES WITH THE PLAYER
            rock.transform.position = Vector3.Lerp(rStartPosition, bulletLocation, time);
            time += Time.deltaTime / bulletTravelTime;

            yield return null;
        }

        rock.gameObject.GetComponent<MeshRenderer>().enabled = false;
        Destroy(rock.gameObject, bulletTravelTime);
    }
}