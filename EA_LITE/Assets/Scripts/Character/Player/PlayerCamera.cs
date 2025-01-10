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
    [SerializeField] float cameraCollisionRadius = 0.2f; // HOW CLOSE THE CAMERA CAN GET BEFORE COLLIDING WITH OBJECT
    [SerializeField] LayerMask collideWithLayers;

    // JUST DISPLAYS CAMERA VALUES
    [Header("Camera Values")]
    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPosition; // USED FOR CAMERA COLLISIONS (MOVES THE CAMERA OBJECT TO THIS POSITION UPON COLLISION)
    [SerializeField] float leftAndRightLookAngle;
    [SerializeField] float upAndDownLookAngle;
    private float cameraZPosition; // VALUES USED FOR CAMERA COLLISIONS
    private float targetCameraZPosition; // VALUES USED FOR CAMERA COLLISIONS

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

        cameraZPosition = cameraObject.transform.localPosition.z;
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
            HandleCollisions();
        }
    }

    private void HandleFollowTarget()
    {
        Vector3 targetCameraZPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraZPosition;
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

    private void HandleCollisions()
    {
        targetCameraZPosition = cameraZPosition;
        RaycastHit hit;
        // DIRECTION OF COLLISION CHECK
        Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
        direction.Normalize();

        // CHECK IF THERE IS AN OBJECT IN FRONT OF THE DESIRED DIRECTION OF CAMERA
        if(Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
        {
            // IF THERE IS, WE GET OUR DISTANCE FROM IT
            float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
            // WE THEN EQUATE OUR TARGET Z POSITION TO THE FOLLOWING
            targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
        }

        // IF OUR TARGET POSITION IS LESS THAN OUR COLLISION RADIUS, WE SUBTRACT OUR COLLISION RADIUS (MAKING IT SNAP BACK)
        if(Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
        {
            targetCameraZPosition = -cameraCollisionRadius;
        }

        // WE THEN APPLY OUR FINAL POSITION USING A LERP OVER A TIME OF 0.2F
        cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
        cameraObject.transform.localPosition = cameraObjectPosition;
    }
}
