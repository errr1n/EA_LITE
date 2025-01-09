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
    [SerializeField] float walkingSpeed = 2;
    [SerializeField] float runningSpeed= 5;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }



    public void HandleAllMovement()
    {
        //GROUNDED MOVEMENT
        HandleGroundedMovement();
        // AERIAL MOVEMENT
        // JUMPING MOVEMENT
        // ROTATION
        // FALLING
    }

    private void GetVerticalAndHorizontalInputs()
    {
        verticalMovement = PlayerInputManager.instance.verticalInput;
        horizontalMovement = PlayerInputManager.instance.horizontalInput;

        // CLAMP THE MOVEMENTS (ANIMATIONS)
    }

    private void HandleGroundedMovement()
    {
        GetVerticalAndHorizontalInputs();

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
}
