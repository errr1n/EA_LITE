using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;
    [HideInInspector] public CharacterAnimatorManager characterAnimatorManager;
    [HideInInspector] public CharacterEffectsManager characterEffectsManager;
    [HideInInspector] public CharacterSoundFXManager characterSoundFXManager;
    [HideInInspector] public CharacterCombatManager characterCombatManager;
    [HideInInspector] public CharacterStatsManager characterStatsManager;
    [HideInInspector] public CharacterLocomotionManager characterLocomotionManager;

    [HideInInspector] public MeleeWeaponDamageCollider damageCollider;

    [Header("Character Group")]
    public CharacterGroup characterGroup;

    [Header("FLAGS")]
    public bool isPerformingAction = false;
    public bool isJumping = false;
    // public bool isGrounded = true;
    // public bool applyRootMotion = false;
    // public bool isGrounded = true;
    // public bool canRotate = true;
    // public bool canMove = true;

    [SerializeField] public bool _isLockedOn = false;
    public bool IsLockedOn{
        get{return _isLockedOn;}
        set{
            OnIsLockedOnChanged(_isLockedOn, value);
            _isLockedOn = value;
        }
    }

    [SerializeField] public bool _isMoving = false;
    public bool IsMoving{
        get{return _isMoving;}
        set{
            OnIsMovingChanged(_isMoving, value);
            _isMoving = value;
        }
    }

    [Header("STATUS")]
    public bool isDead = false;

    [Header("MORE FLAGS")]
    [SerializeField] public bool isSprinting = false;

    protected virtual void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        characterEffectsManager = GetComponent<CharacterEffectsManager>();
        characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
        characterStatsManager = GetComponent<CharacterStatsManager>();
        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
        characterCombatManager = GetComponent<CharacterCombatManager>();
        characterLocomotionManager = GetComponent<CharacterLocomotionManager>();

        damageCollider = GetComponent<MeleeWeaponDamageCollider>();
    }

    protected virtual void Start()
    {
        IgnoreMyOwnColiders();
    }

    protected virtual void Update()
    {
        HandleStatUpdates();

        characterStatsManager.CheckHP();
    }

    protected virtual void FixedUpdate()
    {
        //
    }

    protected virtual void LateUpdate()
    {
        //
    }

    protected virtual void HandleStatUpdates()
    {
        // HEALTH
        if(characterStatsManager.currentVitality != characterStatsManager.newVitality)
        {
            // UPDATES CURRENT VITALITY TO NEW VITALTY
            characterStatsManager.currentVitality = characterStatsManager.newVitality;
            // UPDATES MAX HEALTH BASED ON VITALTY
            characterStatsManager.maxHealth = characterStatsManager.CalculateHealthBasedOnVitalityLevel(characterStatsManager.currentVitality);
            // SETS HEALTH TO FULL WHEN UPDATING MAX HEALTH 
            characterStatsManager.CurrentHealth = characterStatsManager.CalculateHealthBasedOnVitalityLevel(characterStatsManager.currentVitality);
        }
    }

    public virtual IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
    {
        characterStatsManager.CurrentHealth = 0;
        isDead = true;

        // RESET ANY FLAGS THAT NEED TO BE RESET

        // IF WE ARE NOT GROUNDED, PLAY AERIAL DEATH ANIMATION

        if(!manuallySelectDeathAnimation)
        {
            // ANIMATION
            characterAnimatorManager.PlayTargetActionAnimation("Death", true);
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
        CharacterManager damagedCharacterID,
        CharacterManager characterCausingDamageID,  
        float physicalDamage,
        float angleHitFrom,
        float contactPointX,
        float contactPointY,
        float contactPointZ)
    {
        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);

        damageEffect.physicalDamage = physicalDamage;
        damageEffect.angleHitFrom = angleHitFrom;
        damageEffect.contactPoint = new Vector3(contactPointX, contactPointY, contactPointZ);
        Debug.Log("DAMAGED CHARACTER: " + damagedCharacterID + " ATTACKER: " + characterCausingDamageID);
        Debug.Log("contact point:   X: " + contactPointX + "  Y: " + contactPointY + "  Z: " + contactPointZ);
        //

        damagedCharacterID.characterEffectsManager.ProcessInstantEffect(damageEffect);

    }

    // public void OnLockOnTargetIDChange(ulong oldID, ulong newID)
    // {
    //     // character.characterCombatManager.currentTarget = 
    // }

    public void OnIsLockedOnChanged(bool oldValue, bool IsLockedOn)
    {
        if(!IsLockedOn)
        {
            characterCombatManager.currentTarget = null;
        }
    }

    public void OnIsMovingChanged(bool oldStatus, bool newStatus)
    {
        animator.SetBool("isMoving", IsMoving);
    }
}
