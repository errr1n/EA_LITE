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

    [Header("PLAYER MOVEMENT INPUT")]
    [SerializeField] Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    [Header("CAMERA ROTATION INPUT")]
    [SerializeField] Vector2 cameraInput;
    public float cameraVerticalInput;
    public float cameraHorizontalInput;

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
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        // IF WE ARE LOADING INTO OUR WORLD SCENE, ENABLE PLAYER INPUTS
        if(newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
        {
            instance.enabled = true;
        }
        // OTHERWISE DISSABLE IN MENU
        // CANT CONTROL CHARACTER IN OTHER SCREENS  
        else
        {
            instance.enabled = false;
        }
    }

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            // new player input system
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            // new camera input system
            playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();

            // Debug.Log("READ PLAYER CONTROLS");
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
        HandlePlayerMovementInput();
        HandleCameraMovementInput();
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

        // WE PASS 0 ON HORIZONTAL BECAUSE NOT LOCKED ON (NON-STRAFING MOVEMENT)
        // HORIZONTAL WILL BE FOR STRAFING OR LOCKED ON

        if(player == null)
        {
            return;
        }

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
}
