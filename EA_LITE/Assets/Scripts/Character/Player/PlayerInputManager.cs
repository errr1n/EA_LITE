using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{

    public static PlayerInputManager instance;

    PlayerControls playerControls;

    public PlayerManager player;

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
    [SerializeField] bool leftClickInput = false;
    [SerializeField] bool weaponSwapInput = false;

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
            
            // Attack Input
            playerControls.PlayerActions.LeftClick.performed += i => leftClickInput = true;

            // Lock On Input
            playerControls.PlayerActions.LockOn.performed += i => lockOnInput = true;

            // player actions sprint from player controls input system
            // HOLDING THE INPUT, SETS BOOL TO TRUE
            playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
            // RELEASING THE INPUT, SETS BOOL TO FALSE
            playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;

            // Weapon Swap Input
            playerControls.PlayerActions.WeaponSwap.performed += i => weaponSwapInput = true;
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
        // can disable lock on from here
        HandleLockOnInput();
        HandleCameraMovementInput();
        HandlePlayerMovementInput();
        HandleDodgeInput();
        HandleSprintInput();
        HandleLeftClickInput();
        HandleTabPushInput();
    }

    private void HandleLockOnInput()
    {
        // check for dead target
        if(player.IsLockedOn)
        {
            // if no current target
            if(player.playerCombatManager.currentTarget == null)
            {
                return;
            }

            // IS OUR CURRENT TARGET DEAD? (UNLOCK)
            if(player.playerCombatManager.currentTarget.isDead)
            {
                player.IsLockedOn = false;
            }
        }

        // ARE WE ALREADY LOCKED ON? (UNLOCK)
        if(lockOnInput && player.IsLockedOn)
        {
            // set lock on input to false
            lockOnInput = false;
            // reset the lock on target
            PlayerCamera.instance.ClearLockOnTarget();
            //DISABLE LOCK ON
            player.IsLockedOn = false;
            return;
        }
        
        // ENABLE LOCK ON
        if(lockOnInput && !player.IsLockedOn)
        {
            // set lock on input to false
            lockOnInput = false;

            //IF WE ARE USING A RANGED WEAPON NO LOCK ON

            //ATTEMPT TO LOCK ON
            PlayerCamera.instance.HandleLocatingLockOnTargets();

            if(PlayerCamera.instance.nearestLockOnTarget != null)
            {
                // SET THE TARGET AS OUR CURRENT TARGET
                player.playerCombatManager.SetTarget(PlayerCamera.instance.nearestLockOnTarget);
                // enable lock on
                player.IsLockedOn = true;
            }
        }
    }

    private void HandlePlayerMovementInput()
    {
        verticalInput = movementInput.y;
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

        if(moveAmount != 0)
        {
            player.IsMoving = true;
        }
        else
        {
            player.IsMoving = false;
            // might need to adjust here for idle -> move animation issues
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

            // set hand to right
            player.SetCharacterActionHand(true);

            // perform weapon based action
            player.playerCombatManager.PerformWeaponBasedAction(player.playerInventoryManager.currentRightHandWeapon.leftClick_Action, player.playerInventoryManager.currentRightHandWeapon);

        }
    }

    private void HandleTabPushInput()
    {
        if(weaponSwapInput)
        {
            weaponSwapInput = false;

            // CHANGE WEAPON IN HAND
            player.playerEquipmentManager.SwitchRightWeapon();
        }
    }
}
