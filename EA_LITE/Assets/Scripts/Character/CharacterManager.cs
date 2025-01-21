using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // [Header("STATUS")]


    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;
    [HideInInspector] public CharacterAnimatorManager characterAnimatorManager;
    [HideInInspector] public CharacterEffectsManager characterEffectsManager;
    [HideInInspector] public CharacterStatsManager characterStatsManager;
    [HideInInspector] public CharacterCombatManager characterCombatManager;

    [Header("FLAGS")]
    public bool isPerformingAction = false;
    public bool isJumping = false;
    public bool isGrounded = true;
    public bool applyRootMotion = false;
    public bool canRotate = true;
    public bool canMove = true;

    [Header("MORE FLAGS")]
    [SerializeField] public bool isSprinting = false;

    // [Header("STATS")]
    // public int endurance = 1;
    // public int currentStamina = 0;
    // public int maxStamina = 1;

    protected virtual void Awake()
    {
        // DontDestroyOnLoad(this);

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        characterEffectsManager = GetComponent<CharacterEffectsManager>();
        characterStatsManager = GetComponent<CharacterStatsManager>();
        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
        characterCombatManager = GetComponent<CharacterCombatManager>();
    }

    protected virtual void Start()
    {
        IgnoreMyOwnColiders();
    }

    protected virtual void Update()
    {
        // boolean for isGrounded in animator
        // ProcessCharacterDamage();
    }

    protected virtual void FixedUpdate()
    {
        //
    }

    protected virtual void LateUpdate()
    {
        //
    }

    public virtual IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
    {
        characterStatsManager.CurrentHealth = 0;
        characterStatsManager.isDead = true;
        // Debug.Log("HERE, DEFINETLY DEAD");

        // RESET ANY FLAGS THAT NEED TO BE RESET

        // IF WE ARE NOT GROUNDED, PLAY AERIAL DEATH ANIMATION

        if(!manuallySelectDeathAnimation)
        {
            // ANIMATION
            // characterAnimatorManager.PlayTargetActionAnimation("Dead_01", true);
        }

        // PLAY SOME DEATH SFX

        yield return new WaitForSeconds(5);

        // DISABLE CHARACTER
    }

    public virtual void ReviveCharacter()
    {
        //
    }

    protected virtual void IgnoreMyOwnColiders()
    {
        Collider characterControllerCollider = GetComponent<Collider>();
        Collider[] damageableCharacterColliders = GetComponentsInChildren<Collider>();
        List<Collider> ignoreColiders = new List<Collider>();

        // ADD ALL OF OUR DAMAGEABLE CHARACTER COLLIDERS, TO THE LIST THAT WILL BE USED TO IGNORE COLISIONS
        foreach(var collider in damageableCharacterColliders)
        {
            ignoreColiders.Add(collider);
        }

        // ADDS OUR CHARACTER CONTROLLER TO THE LIST THE WILL BE USED TO IGNORE COLLISIONS
        ignoreColiders.Add(GetComponent<Collider>());

        // GOES THROUGH EVERY COLLIDER ON THE LIST, AND IGNORES COLLISIONS WITH EACH OTHER
        foreach(var collider in ignoreColiders)
        {
            foreach(var otherCollider in ignoreColiders)
            {
                Physics.IgnoreCollision(collider, otherCollider, true);
            }
        }
    }

    public void ProcessCharacterDamage(
        CharacterManager damagedCharacter,  
        float physicalDamage,
        float angleHitFrom,
        float contactPointX,
        float contactPointY,
        float contactPointZ)
    {
        // damagedCharacter = damagedCharacter;

        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
        damageEffect.physicalDamage = physicalDamage;
        damageEffect.angleHitFrom = angleHitFrom;
        damageEffect.contactPoint = new Vector3(contactPointX, contactPointY, contactPointZ);
        // damageEffect.characterCausingDamage = characterCausingDamage;

        damagedCharacter.characterEffectsManager.ProcessInstantEffect(damageEffect);
        Debug.Log("SOMEHOW WORKING?");

    }
}
