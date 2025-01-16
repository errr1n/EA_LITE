using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator animator;

    [Header("FLAGS")]
    public bool isPerformingAction = false;
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
    }

    // protected virtual void Start()
    // {
    //     //
    // }

    protected virtual void Update()
    {

    }

    protected virtual void LateUpdate()
    {
        //
    }
}
