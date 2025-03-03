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

    [Header("Character Group")]
    public CharacterGroup characterGroup;

    [Header("FLAGS")]
    public bool isPerformingAction = false;
    public bool isJumping = false;
    public bool isInvulnerable = false;
    public bool isSprinting = false;

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

    protected virtual void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
        characterEffectsManager = GetComponent<CharacterEffectsManager>();
        characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
        characterCombatManager = GetComponent<CharacterCombatManager>();
        characterStatsManager = GetComponent<CharacterStatsManager>();
        characterLocomotionManager = GetComponent<CharacterLocomotionManager>();
    }

    protected virtual void Start()
    {
        IgnoreMyOwnColiders();
    }

    protected virtual void Update()
    {
        Debug.Log("2/4 Character manager update"); // CALLED AGAIN AFTER PLAYER MANAGER BASE UPDATE AND BEFORE "END OF WAIT"
        HandleStatUpdates();

        // GET HP VALUES FROM CHARACTERS
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
        Debug.Log("ProcessDeathEvent charactermanager");
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
        Debug.Log("5 END OF WAIT");

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
        // call the call damage effect
        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);

        // get physical damage amount
        damageEffect.physicalDamage = physicalDamage;
        // get angle hit from
        damageEffect.angleHitFrom = angleHitFrom;
        // get hit contact point
        damageEffect.contactPoint = new Vector3(contactPointX, contactPointY, contactPointZ);

        // Debug.Log("DAMAGED CHARACTER: " + damagedCharacterID + " ATTACKER: " + characterCausingDamageID);
        // Debug.Log("contact point:   X: " + contactPointX + "  Y: " + contactPointY + "  Z: " + contactPointZ);

        damagedCharacterID.characterEffectsManager.ProcessInstantEffect(damageEffect);

    }

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
