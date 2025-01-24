using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{

    public static PlayerInputManager instance;

    public PlayerManager player;

    // 1. READ VALUES OF JOYSTICK AND WASD
    // 2. MOVE CHARACTER BASED ON THOSE VALUES

    PlayerControls playerControls;

    [Header("CAMERA ROTATION INPUT")]
    [SerializeField] Vector2 cameraInput;
    public float cameraVerticalInput;
    public float cameraHorizontalInput;

    [Header("LOCK ON INPUT")]
    [SerializeField] bool lockOnInput;

    [Header("PLAYER MOVEMENT INPUT")]
    [SerializeField] Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    [Header("PLAYER ACTION INPUT")]
    [SerializeField] bool dodgeInput = false;
    [SerializeField] bool sprintInput = false;
    // [SerializeField] bool jumpInput = false;
    // public bool isSprinting = false;
    [SerializeField] bool leftClickInput = false;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // // WHEN THE SCENE CHANGES, THIS FUNCTION IS RUNNING
        // SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        // WHEN THE SCENE CHANGES, THIS FUNCTION IS RUNNING
        SceneManager.activeSceneChanged += OnSceneChange;

        // instance.enabled = false;

        // if(playerControls != null)
        // {
        //     playerControls.Disable();
        // }
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        // IF WE ARE LOADING INTO OUR WORLD SCENE, ENABLE PLAYER INPUTS
        if(newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
        {
            instance.enabled = true;

            // if(playerControls != null)
            // {
            //     playerControls.Enable();
            // }
        }
        // OTHERWISE DISSABLE IN MENU
        // CANT CONTROL CHARACTER IN OTHER SCREENS  
        else
        {
            instance.enabled = false;

            // if(playerControls != null)
            // {
            //     playerControls.Disable();
            // }
        }
    }

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            // player controls from new input system
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            // camera controls from player controls input system
            playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
            // player actions dodge from player controls input system
            playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;

            playerControls.PlayerActions.LeftClick.performed += i => leftClickInput = true;

            playerControls.PlayerActions.LockOn.performed += i => lockOnInput = true;

            // player actions sprint from player controls input system
            // HOLDING THE INPUT, SETS BOOL TO TRUE
            playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
            // RELEASING THE INPUT, SETS BOOL TO FALSE
            playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;
        }

        playerControls.Enable();
    }

    private void OnDestroy()
    {
        // IF WE DESTROY THIS OBJECT, UNSUBSCRIBE FROM THIS EVENT
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    //APPLICATION MINIMIZED VS OPEN
    // private void OnApplicationFocus(bool focus)
    // {
    //     if(enabled)
    //     {
    //         if(focus)
    //         {
    //             playerControls.Enable();
    //         }
    //         else
    //         {
    //             PlayerControls.Disable();
    //         }
    //     }
    // }

    private void Update()
    {
        HandleAllInput();
    }

    private void HandleAllInput()
    {
        // HandleLockOnInput();
        HandleCameraMovementInput();
        HandlePlayerMovementInput();
        HandleDodgeInput();
        HandleSprintInput();
        HandleLeftClickInput();
    }

    private void HandleLockOnInput()
    {
        // check for dead target
        if(player.IsLockedOn)
        {
            if(player.playerCombatManager.currentTarget == null)
            {
                // lockOnInput = false;
                return;
            }

            // IS OUR CURRENT TARGET DEAD? (UNLOCK)
            if(player.playerCombatManager.currentTarget.isDead)
            {
                player.IsLockedOn = false;
            }

            //attempt to find new target
            // lockOnInput = false;
        }

        if(lockOnInput && player.IsLockedOn)
        {
            lockOnInput = false;
            PlayerCamera.instance.ClearLockOnTarget();
            // Debug.Log("CLEAR LOCK ON");
            player.IsLockedOn = false;
            //ARE WE ALREADY LOCKED ON? (UNLOCK)

            //ATTEMPT TO LOCK ON

            //DISABLE LOCK ON
            return;
        }

        if(lockOnInput && !player.IsLockedOn)
        {
            lockOnInput = false;

            //IF WE ARE USING A RANGED WEAPON NO LOCK ON

            //ATTEMPT TO LOCK ON
            PlayerCamera.instance.HandleLocatingLockOnTargets();

            if(PlayerCamera.instance.nearestLockOnTarget != null)
            {
                // SET THE TARGET AS OUR CURRENT TARGET
                player.playerCombatManager.SetTarget(PlayerCamera.instance.nearestLockOnTarget);
                player.IsLockedOn = true;
                // Debug.Log("is locked on: " + player.IsLockedOn);
            }
        }

        // if(player.playerCombatManager.currentTarget != null)
        // {
        //     // lockOnInput = false;
        //     return;
        // }

        // if(!lockOnInput && player.isLockedOn)
        // {
        //     lockOnInput = false;
        // }
    }

    private void HandlePlayerMovementInput()
    {
        // Debug.Log("HandlePlayerMovementInput()");
        verticalInput = movementInput.y;
        // Debug.Log(movementInput.y);
        horizontalInput = movementInput.x;

        // ABS MAKES VALUE ALWAYS POSITIVE (ADDING TOGETHER TOTAL WITHOUT NEGATIVE SIGN)
        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

        // WE CLAMP THE VALUES, SO THEY ARE 0. 0.5, OR 1
        if(moveAmount <= 0.5 && moveAmount > 0)
        {
            moveAmount = 0.5f;
        }
        else if(moveAmount > 0.5 && moveAmount <= 1)
        {
            moveAmount = 1;
        }

        if(player == null)
        {
            return;
        }

        //idk if needed yet
        if(moveAmount != 0)
        {
            player.IsMoving = true;
            // Debug.Log(player.IsMoving);
        }
        else
        {
            player.IsMoving = false;
        }

        // WE PASS 0 ON HORIZONTAL BECAUSE NOT LOCKED ON (NON-STRAFING MOVEMENT)
        // HORIZONTAL WILL BE FOR STRAFING OR LOCKED ON

        // IF WE ARE NOT LOCKED ON, ONLY USE MOVE AMOUNT
        player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);

        //IF WE ARE LOCKED ON, PASS HORIZONTAL AND VERTICAL VALUES
    }

    private void HandleCameraMovementInput()
    {
        // CAN ADJUST SENSITIVITY HERE - PREFERABLY DONE IN PLAYER CAMERA SCRIPT
        cameraVerticalInput = cameraInput.y;
        cameraHorizontalInput = cameraInput.x;
    }

    private void HandleDodgeInput()
    {
        if(dodgeInput)
        {
            dodgeInput = false;
            // ATTEMPT TO PERFORM A DODGE
            player.playerLocomotionManager.AttemptToPerformDodge();
        }
    }

    private void HandleSprintInput()
    {
        if(sprintInput)
        {
            // HANDLE SPRINTING
            player.playerLocomotionManager.SprintOn();
            // Debug.Log("HERE");
        }
        else
        {
            player.playerLocomotionManager.SprintOff();
        }
    }

    // private void HandleJumpInput()
    // {
    //     if(jumpInput)
    //     {
    //         jumpInput = false;
    //         // ATTEMPT TO PERFORM A JUMP
    //         player.playerLocomotionManager.AttemptToPerformJump();
    //     }
    // }

    private void HandleLeftClickInput()
    {
        if(leftClickInput)
        {
            leftClickInput = false;

            player.playerEquipmentManager.SetCharacterActionHand(true);

            player.playerCombatManager.PerformWeaponBasedAction(player.playerInventoryManager.currentRightHandWeapon.leftClick_Action, player.playerInventoryManager.currentRightHandWeapon);

        }
    }
}
