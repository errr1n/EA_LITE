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
    [HideInInspector] public CharacterSoundFXManager characterSoundFXManager;
    [HideInInspector] public CharacterCombatManager characterCombatManager;
    [HideInInspector] public CharacterStatsManager characterStatsManager;

    [Header("Character Group")]
    public CharacterGroup characterGroup;

    [Header("FLAGS")]
    public bool isPerformingAction = false;
    public bool isJumping = false;
    public bool isGrounded = true;
    public bool applyRootMotion = false;
    public bool canRotate = true;
    public bool canMove = true;

    [SerializeField] public bool isLockedOn = false;
    public bool IsLockedOn{
        get{return isLockedOn;}
        set{
            OnIsLockedOnChanged(isLockedOn, value);
            isLockedOn = value;
        }
    }

    [SerializeField] public bool isMoving = false;
    public bool IsMoving{
        get{return isMoving;}
        set{
            OnIsMovingChanged(isMoving, value);
            isMoving = value;
            // Debug.Log("isMoving: " + isMoving);
        }
    }

    [Header("STATUS")]
    public bool isDead = false;

    [Header("MORE FLAGS")]
    [SerializeField] public bool isSprinting = false;

    // [Header("STATS")]
    // public int endurance = 1;
    // public int currentStamina = 0;
    // public int maxStamina = 1;

    // public ulong _currentLockOnTargetID = 0;
    // public ulong CurrentLockOnTargetID{
    //     get{return _currentLockOnTargetID;}
    //     set{
    //         // UPDATES HEALTH UI BAR WHEN HEALTH CHANGES 
    //         OnLockOnTargetIDChange(_currentLockOnTargetID, value);
    //         // Debug.Log("---VALUE---: " + value);
    //         _currentLockOnTargetID = value;
    //         // Debug.Log("CURRENT HEALTH: " + _currentRightHandWeaponID);
    //     }
    // }

    protected virtual void Awake()
    {
        // DontDestroyOnLoad(this);

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        characterEffectsManager = GetComponent<CharacterEffectsManager>();
        characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
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
        isDead = true;
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

    public void OnLockOnTargetIDChange(ulong oldID, ulong newID)
    {
        // character.characterCombatManager.currentTarget = 
    }

    public void OnIsLockedOnChanged(bool oldValue, bool IsLockedOn)
    {
        if(!IsLockedOn)
        {
            characterCombatManager.currentTarget = null;
        }
    }

    // MOVED TO CHARACTER STATS MANAGER
    public void OnIsMovingChanged(bool oldStatus, bool newStatus)
    {
        animator.SetBool("isMoving", IsMoving);
        // Debug.Log("OnIsMovingChanged: " + IsMoving);
    }
}
