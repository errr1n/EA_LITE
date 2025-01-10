using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public PlayerManager player;

    public Camera cameraObject;

    [Header("Camera Settings")]
    private Vector3 cameraVelocity;
    private float cameraSmoothSpeed = 1; // THE BIGGER THIS NUMBER, THE LONGER IT TKAES CAMERA TO REACH ITS POSITION DURING MOVEMENT

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
            FollowTarget();
            // ROTATE AROUND PLAYER
            // COLLIDE WITH ENVIRONMENT
        }
    }

    private void FollowTarget()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }
}
