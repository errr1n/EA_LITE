using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;

    // TAKEN FROM INPUT MANAGER
    public float verticalMovement;
    public float horizontalMovement;
    public float moveAmount;

    private Vector3 moveDirection;
    private Vector3 targetRotationDirection;
    [SerializeField] float walkingSpeed = 2;
    [SerializeField] float runningSpeed = 5;
    [SerializeField] float rotationSpeed = 15;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
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
        GetMovementValues();

        // OUR MOVEMENT DIRECTION IS BASED ON OUR CAMERAS FACING PERSPECTIVE AND OUR INPUTS
        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
        moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if(PlayerInputManager.instance.moveAmount > 0.5f)
        {
            // Debug.Log("HERE");
            // MOVE AT RUNNING SPEED
            player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
        }
        else if(PlayerInputManager.instance.moveAmount <= 0.5f)
        {
            // MOVE AT WALKING SPEED
            player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
        }
    }

    private void HandleRotation()
    {
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
}
