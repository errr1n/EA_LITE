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
    // [SerializeField] float sprintingStaminaCost = 2;

    [Header("DODGE")]
    private Vector3 rollDirection;
    // [SerializeField] float dodgeStaminaCost = 25;

    [Header("JUMP")]
    // [SerializeField] float jumpStaminaCost = 25;
    // [SerializeField] float jumpHeight = 2;
    // [SerializeField] float jumpForwardSpeed = 5;
    [SerializeField] float freeFallSpeed = 2;
    private Vector3 jumpDirection;

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
        // HandleJumpingMovement();
        // JUMPING MOVEMENT
        // ROTATION
        // FALLING
        HandleFreeFallMovement();
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
        if(!player.playerLocomotionManager.canMove)
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

    // private void HandleJumpingMovement()
    // {
    //     if(player.isJumping)
    //     {
    //         // Debug.Log("JUMPING MOVEMENT");
    //         player.characterController.Move(jumpDirection * jumpForwardSpeed * Time.deltaTime);
    //     }
    // }

    private void HandleFreeFallMovement()
    {
        if(!isGrounded)
        {
            // Debug.Log("FREE FALL MOVEMENT");
            Vector3 freeFallDirection;

            freeFallDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput;
            freeFallDirection += PlayerCamera.instance.cameraObject.transform.right * PlayerInputManager.instance.horizontalInput;
            freeFallDirection.y = 0;

            player.characterController.Move(freeFallDirection * freeFallSpeed * Time.deltaTime);
        }
    }

    private void HandleRotation()
    {
        if(!canRotate)
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

    public void SprintOn()
    {
        if(player.isPerformingAction)
        {
            // SET SPRINTING TO FALSE
            player.isSprinting = false;
        }

        // IF WE ARE MOVING, SET SPRINTING TO TRUE
        if(moveAmount >= 0.5)
        {
            //SPRINTING
            player.isSprinting = true;
        }
        else
        {
            SprintOff();
        }

        // STAMINA
        // if(characterStatsManager.CurrentStamina <= 0)
        // {
        //     // SET SPRINTING TO FALSE
        //     player.isSprinting = false;
        //     return;
        // }

        // if(player.isSprinting)
        // {
        //     characterStatsManager.CurrentStamina -= sprintingStaminaCost * Time.deltaTime;
        // }
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

        // if(characterStatsManager.CurrentStamina <= 0)
        // {
        //     return;
        // }

        // CAN ONLY ROLL WHEN ALREADY MOVING, NOT WHEN STATIONARY
        if(PlayerInputManager.instance.moveAmount > 0)
        {
            // player.isPerformingAction = true;

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
            // player.isPerformingAction = true;
            // DO WE WANT A STATIONARY DODGE? AKA BACKSTEP
            player.playerAnimatorManager.PlayTargetActionAnimation("BackStep", true, true);
        }

        // characterStatsManager.CurrentStamina -= dodgeStaminaCost;
    }

    // public void AttemptToPerformJump()
    // {
    //     // IF WE ARE PERFORMING AN ACTION, WE DO NOT WANT TO ALLOW JUMP (UNLESS JUMP ATTACK)
    //     if(player.isPerformingAction)
    //     {
    //         return;
    //     }

    //     // IF WE ARE OUT OF STAMINA, CAN'T JUMP?
    //     // if(characterStatsManager.CurrentStamina <= 0)
    //     // {
    //     //     return;
    //     // }

    //     // IF WE ARE ALREADY IN A JUMP, WE DO NOT WANT TO ALLOW A JUMP AGAIN UNTIL THE CURRENT JUMP HAS FINISHED 
    //     if(player.isJumping)
    //     {
    //         return;
    //     }

    //     // IF WE ARE NOT GROUNDED, WE DO NOT WANT TO ALLOW A JUMP
    //     if(!player.isGrounded)
    //     {
    //         return;
    //     }

    //     // ANIMATOR HERE FOR JUMPING ANIMATION / FALLING ANIMATION

    //     player.isJumping = true;
    //     Debug.Log("HERE");

    //     // characterStatsManager.CurrentStamina -= jumpStaminaCost;

    //     // get the direction based on the camera
    //     jumpDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
    //     jumpDirection = jumpDirection + PlayerCamera.instance.transform.right * horizontalMovement;
    //     jumpDirection.y = 0;

    //     if(jumpDirection != Vector3.zero)
    //     {
    //         // VARIABLE JUMP DISTANCE DEPENDING ON HOW FAST PLAYER IS MOVING
    //         if(player.isSprinting)
    //         {
    //             jumpDirection = jumpDirection * 1;
    //         }
    //         else if(moveAmount >= 0.5)
    //         {
    //             jumpDirection = jumpDirection * 0.5f;
    //         }
    //         else if(moveAmount <= 0.5)
    //         {
    //             jumpDirection = jumpDirection * 0.25f;
    //         }
    //     }
    // }

    // public void ApplyJumpingVelocity()
    // {
    //     // APPLY AN UPWARD VELOCITY DEPENDING ON FORCES IN THE GAME (GRAVITY)
    // }
}
