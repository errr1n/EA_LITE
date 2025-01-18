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
    }

    // protected virtual void Start()
    // {
    //     //
    // }

    protected virtual void Update()
    {
        // boolean for isGrounded in animator
    }

    protected virtual void LateUpdate()
    {
        //
    }

    public virtual IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
    {
        characterStatsManager.CurrentHealth = 0;
        characterStatsManager.isDead = true;
        Debug.Log("HERE, DEFINETLY DEAD");

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
}
