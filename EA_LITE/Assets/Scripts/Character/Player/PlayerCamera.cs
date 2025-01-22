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
    [SerializeField] float leftAndRightRotationSpeed = 120; // 220
    [SerializeField] float upAndDownRotationSpeed = 120; //220
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

    [Header("Lock On")]
    [SerializeField] float lockOnRadius = 20;
    [SerializeField] float minimumViewableAngle = -50;
    [SerializeField] float maximumViewableAngle = 50;
    // [SerializeField] float maximumLockOnDistance = 20;
    private List<CharacterManager> availableTargets = new List<CharacterManager>();
    public CharacterManager nearestLockOnTarget;
    [SerializeField] float lockOnTargetFollowSpeed = 0.2f;

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
        if(player.isLockedOn)
        {
            // THIS ROTATES THIS GAMEOBJECT (left and right)
            Vector3 rotationDirection = player.playerCombatManager.currentTarget.characterCombatManager.lockOnTransform.position - transform.position;
            rotationDirection.Normalize();
            rotationDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lockOnTargetFollowSpeed);

            //THIS ROTATES THE PIVOT OBJECT (up and down)
            //gets lock on from characerCombatManager on the target
            rotationDirection = player.playerCombatManager.currentTarget.characterCombatManager.lockOnTransform.position - cameraPivotTransform.position;
            rotationDirection.Normalize();

            targetRotation = Quaternion.LookRotation(rotationDirection);
            cameraPivotTransform.transform.rotation = Quaternion.Slerp(cameraPivotTransform.rotation, targetRotation, lockOnTargetFollowSpeed);

            // SAVE OUR ROTATION VALUES TO OUT LOOK ANGLES SO WHEN WE UNLOCK IT DOESNT SNAP TOO FAR AWAY
            leftAndRightLookAngle = transform.eulerAngles.y;
            upAndDownLookAngle = transform.eulerAngles.x;
        }
        // ELSE ROTATE REGULARLY
        else
        {
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

    public void HandleLocatingLockOnTargets()
    {
        float shortDistance = Mathf.Infinity;                   // WILL BE USED TO DETERMINE THE TARGET CLOSEST TO THE PLAYER
        float shortDistanceOfRightTarget = Mathf.Infinity;      // WILL BE USED TO DETERMINE THE SHORTEST DISTANCE ON ONE AXIS TO THE RIGHT OF CURRENT TARGET (+) (move to right in inspector = positive)
        float shortDistanceOfLefttTarget = -Mathf.Infinity;     // WILL BE USED TO DETERMINE SHORTEST DISTANCE ON ONE AXIS TO THE LEFT OF THE CURRENT TARGET (-) (move to left in inspector = negative)

        // ADD LAYER MASK
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, lockOnRadius, WorldUtilityManager.instance.GetCharacterLayers());

        for(int i = 0; i < colliders.Length; i++)
        {
            CharacterManager lockOnTarget = colliders[i].GetComponent<CharacterManager>();

            if(lockOnTarget != null)
            {
                // Debug.Log("NOT NULL");
                //  check if they are within our field of view
                Vector3 lockOnTargetsDirection = lockOnTarget.transform.position - player.transform.position;
                float distanceFromTarget = Vector3.Distance(player.transform.position, lockOnTarget.transform.position);
                float viewableAngle = Vector3.Angle(lockOnTargetsDirection, cameraObject.transform.forward);

                // IF TARGET IS DEAD, CHECK THE NEXT POTENTIAL TARGET
                if(lockOnTarget.isDead)
                {
                    //makes loop go on if true, otherwise ignore
                    continue;
                }

                //ROOT?
                //IF TARGET IS US, CHECK THE NEXT POTENTIAL TARGET
                if(lockOnTarget.transform.root == player.transform.root)
                {
                    continue;
                }

                //IF THE TARGET IS TOO FAR AWAY, CHECK THE NEXT POTENTIAL TARGET
                // if(distanceFromTarget > maximumLockOnDistance)
                // {
                //     continue;
                // }

                // LASTLY, IF THE TARGET IS OUTSIDE OF THE FIELD OF VIEW OR BLOCK BY ENVIRONEMENT, CHECK NEXT POTENTIAL TARGET
                if(viewableAngle > minimumViewableAngle && viewableAngle < maximumViewableAngle)
                {
                    RaycastHit hit;

                    // ONLY CHECK LAYER MASK FOR ENVIRO LAYERS
                    if(Physics.Linecast(player.playerCombatManager.lockOnTransform.position, lockOnTarget.characterCombatManager.lockOnTransform.position, 
                    out hit, WorldUtilityManager.instance.GetEnviroLayers()))
                    {
                        // WE HIT SOMETHING, WE CANNOT SEE OUR LOCK ON TARGET
                        continue;
                    }
                    else
                    {
                        // OTHERWISE ADD THIS TARGET TO POTENTIAL TARGET LIST
                        availableTargets.Add(lockOnTarget);
                        // Debug.Log("ADD AVAILABLE TARGET: " + availableTargets);
                    }
                }
            }

        }

        // WE NOW SORT THROUGH POTENTIAL TARGETS, SEE WHICH ONE IS THE CLOSEST TO LOCK ONTO FIRST
        for(int k = 0; k < availableTargets.Count; k++)
        {
            if(availableTargets[k] != null)
            {
                float distanceFromTarget = Vector3.Distance(player.transform.position, availableTargets[k].transform.position);
                // Vector3 lockTargetDirection = availableTargets[k].transform.position - player.transform.position;

                if(distanceFromTarget < shortDistance)
                {
                    shortDistance = distanceFromTarget;
                    nearestLockOnTarget = availableTargets[k];
                    // Debug.Log("nearestLockOnTarget: " + nearestLockOnTarget);
                }
            }
            else
            {
                ClearLockOnTarget();
                player.IsLockedOn = false;
            }
        }
    }

    public void ClearLockOnTarget()
    {
        nearestLockOnTarget = null;
        availableTargets.Clear();
        // Debug.Log("cleared");
    }
}
