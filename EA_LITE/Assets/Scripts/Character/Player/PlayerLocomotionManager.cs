using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;
    CharacterStatsManager characterStatsManager;

    // TAKEN FROM INPUT MANAGER
    [HideInInspector] public float verticalMovement;
    [HideInInspector] public float horizontalMovement;
    [HideInInspector] public float moveAmount;

    [Header("MOVEMENT SETTINGS")]
    private Vector3 moveDirection;
    private Vector3 targetRotationDirection;
    [SerializeField] float walkingSpeed = 2;
    [SerializeField] float runningSpeed = 5;
    [SerializeField] float sprintingSpeed = 7;
    [SerializeField] float rotationSpeed = 15;
    [SerializeField] float sprintingStaminaCost = 2;

    // [SerializeField] public bool isSprinting = false;

    [Header("DODGE")]
    private Vector3 rollDirection;
    [SerializeField] float dodgeStaminaCost = 25;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
        characterStatsManager = GetComponent<CharacterStatsManager>();
    }

    // protected override void Update()
    // {
    //     base.Update();
    // }


    public void HandleAllMovement()
    {
        

        //GROUNDED MOVEMENT
        HandleGroundedMovement();
        HandleRotation();
        // AERIAL MOVEMENT
        // JUMPING MOVEMENT
        // ROTATION
        // FALLING
    }

    // was GetVerticalAndHorizontalInputs()
    private void GetMovementValues()
    {
        verticalMovement = PlayerInputManager.instance.verticalInput;
        horizontalMovement = PlayerInputManager.instance.horizontalInput;
        moveAmount = PlayerInputManager.instance.moveAmount;

        // CLAMP THE MOVEMENTS (ANIMATIONS)
    }

    private void HandleGroundedMovement()
    {
        if(!player.canMove)
        {
            return;
        }

        GetMovementValues();
        
        // OUR MOVEMENT DIRECTION IS BASED ON OUR CAMERAS FACING PERSPECTIVE AND OUR INPUTS
        // camera direction * movement = forward and back
        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
        // left and right
        moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        // no up and down movement
        moveDirection.y = 0;

        // SPRINT
        if(player.isSprinting)
        {
            // SPRINT SPEED
            player.characterController.Move(moveDirection * sprintingSpeed * Time.deltaTime);
            // Debug.Log("SPRINT");
        }
        else
        {
            // isSprinting = false;
            // RUN VS WALK
            if(PlayerInputManager.instance.moveAmount > 0.5f)
            {
                // MOVE AT RUNNING SPEED
                player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
                // Debug.Log("RUN");
            }
            else if(PlayerInputManager.instance.moveAmount <= 0.5f)
            {
                // MOVE AT WALKING SPEED
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            }
        }

        // // RUN VS WALK
        // if(PlayerInputManager.instance.moveAmount > 0.5f)
        // {
        //     // MOVE AT RUNNING SPEED
        //     player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
        // }
        // else if(PlayerInputManager.instance.moveAmount <= 0.5f)
        // {
        //     // MOVE AT WALKING SPEED
        //     player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
        // }
    }

    private void HandleRotation()
    {
        if(!player.canRotate)
        {
            return;
        }

        Vector3 targetRotationDirection = Vector3.zero;
        targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
        targetRotationDirection = targetRotationDirection + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
        targetRotationDirection.Normalize();
        targetRotationDirection.y = 0;

        if(targetRotationDirection == Vector3.zero)
        {
            targetRotationDirection = transform.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }

    // // only called when sprint button is down
    // public void HandleSprinting()
    // {
    //     isSprinting = false;

    //     if(player.isPerformingAction)
    //     {
    //         // SET SPRINTING TO FALSE
    //         isSprinting = false;
    //         // Debug.Log(isSprinting);
    //     }

    //     // STAMINA?

    //     // IF WE ARE MOVING, SET SPRINTING TO TRUE
    //     if(moveAmount >= 0.5)
    //     {
    //         //SPRINTING
    //         isSprinting = true;
    //         // Debug.Log("SPRINT IS TRUE");
    //         // Debug.Log(moveAmount);
    //     }
    //     else
    //     {
    //         // IF STATIONARY, SET SPRINTING TO FALSE
    //         isSprinting = false;
    //         // Debug.Log("OFF");
    //     }
    //     // isSprinting = false;
    // }

    public void SprintOn()
    {
        if(player.isPerformingAction)
        {
            // SET SPRINTING TO FALSE
            player.isSprinting = false;
            // Debug.Log(isSprinting);
        }

        // STAMINA?
        if(characterStatsManager.CurrentStamina <= 0)
        {
            // SET SPRINTING TO FALSE
            player.isSprinting = false;
            return;
        }

        // IF WE ARE MOVING, SET SPRINTING TO TRUE
        if(moveAmount >= 0.5)
        {
            //SPRINTING
            player.isSprinting = true;
            // Debug.Log("SPRINT IS TRUE");
            // Debug.Log(moveAmount);
        }

        if(player.isSprinting)
        {
            characterStatsManager.CurrentStamina -= sprintingStaminaCost * Time.deltaTime;
            // Debug.Log(characterStatsManager.CurrentStamina);
        }
    }

    public void SprintOff()
    {
        player.isSprinting = false;
    }

    public void AttemptToPerformDodge()
    {
        // CAN'T SPAM DODGE (CAN'T INTERUPT ANIMATION)
        // AKA IF ALREADY ROLLING, DON'T ROLL AGAIN
        if(player.isPerformingAction)
        {
            return;
        }

        if(characterStatsManager.CurrentStamina <= 0)
        {
            return;
        }

        // CAN ONLY ROLL WHEN ALREADY MOVING, NOT WHEN STATIONARY
        if(PlayerInputManager.instance.moveAmount > 0)
        {
            rollDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput;
            rollDirection += PlayerCamera.instance.cameraObject.transform.right * PlayerInputManager.instance.horizontalInput;
            rollDirection.y = 0; // DON'T ROLL ON VERTICAL AXIS
            rollDirection.Normalize();

            Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
            player.transform.rotation = playerRotation;

            // PERFORM ROLL ANIMATION
            // MAY NEED TO CHANGE NAME <-------------------
            // ADJUSTED, MAY WANT TO DISABLE "CAN MOVE" FLAG (ResetActionFlag Script in animator) ---------> player.playerAnimatorManager.PlayTargetActionAnimation("RollForward", true, true); <-------------
            // player.playerAnimatorManager.PlayTargetActionAnimation("RollForward", true, true, false, true);
            // player.characterController.Move(rollDirection * runningSpeed * Time.deltaTime);
            // player.playerAnimatorManager.PlayTargetActionAnimation("RollForward", true, false);
            // Debug.Log(rollDirection * (runningSpeed * 5) * Time.deltaTime);
            player.playerAnimatorManager.PlayTargetActionAnimation("RollForward_2", true, true);
        }
        else
        {
            // DO WE WANT A STATIONARY DODGE? AKA BACKSTEP
            player.playerAnimatorManager.PlayTargetActionAnimation("BackStep", true, true);
        }

        characterStatsManager.CurrentStamina -= dodgeStaminaCost;
    }
}
