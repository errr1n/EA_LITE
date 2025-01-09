using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{

    public static PlayerInputManager instance;

    // 1. READ VALUES OF JOYSTICK AND WASD
    // 2. MOVE CHARACTER BASED ON THOSE VALUES

    PlayerControls playerControls;

    [SerializeField] Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

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

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

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
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        // Debug.Log("HandleMovementInput()");
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
    }
}
