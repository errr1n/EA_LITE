using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public PlayerManager player;

    public Camera cameraObject;
    [SerializeField] Transform cameraPivotTransform;

    // CHANGE THESE TO TWEAK CAMERA PERFORMANCE IN WORLD
    [Header("Camera Settings")]
    private float cameraSmoothSpeed = 1; // THE BIGGER THIS NUMBER, THE LONGER IT TKAES CAMERA TO REACH ITS POSITION DURING MOVEMENT
    [SerializeField] float leftAndRightRotationSpeed = 220;
    [SerializeField] float upAndDownRotationSpeed = 220;
    [SerializeField] float minimumPivot = -30; // THE LOWEST ANGLE WE CAN LOOK DOWN
    [SerializeField] float maximumPivot = 60; // THE HIGHEST ANGLE WE CAN LOOK UP

    // JUST DISPLAYS CAMERA VALUES
    [Header("Camera Values")]
    private Vector3 cameraVelocity;
    [SerializeField] float leftAndRightLookAngle;
    [SerializeField] float upAndDownLookAngle;

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
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void HandleAllCameraActions()
    {
        if(player != null)
        {
            // Debug.Log("follow target");
            //  FOLLOW THE PLAYER
            HandleFollowTarget();
            // ROTATE AROUND PLAYER
            HandleRotations();
            // COLLIDE WITH ENVIRONMENT
        }
    }

    private void HandleFollowTarget()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }

    private void HandleRotations()
    {
        // IF LOCKED ON, FORCE ROTATION TOWARDS TARGET
        // ELSE ROTATE REGULARLY

        // NORMAL ROTATIONS 
        // ROTATE LEFT AND RIGHT BASED ON HORIZONTAL MOVEMENT OF RIGHT JOYSTICK
        leftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
        // ROTATE UP AND DOWN BASED ON VERTICAL MOVEMENT OF RIGHT JOYSTICK
        upAndDownLookAngle -= (PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
        // CLAMP UP AND DOWN LOOK ANGLE BETWEEN MIN AND MAX VALUES
        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

        Vector3 cameraRotation = Vector3.zero;
        Quaternion targetRotation;

        // ROTATE THIS GAMEOBJECT LEFT AND RIGHT
        cameraRotation.y = leftAndRightLookAngle; // IN TERMS OF ROTATION, Y IS LEFT AND RIGHT
        targetRotation = Quaternion.Euler(cameraRotation);
        transform.rotation = targetRotation;

        // ROTATE THE PIVOT GAMEOBJECT UP AND DOWN
        cameraRotation = Vector3.zero;
        cameraRotation.x = upAndDownLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        cameraPivotTransform.localRotation = targetRotation;
    }
}
