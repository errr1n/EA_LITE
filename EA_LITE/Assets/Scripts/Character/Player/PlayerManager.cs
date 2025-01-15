using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
    [HideInInspector] public CharacterStatsManager characterStatsManager;
    // [HideInInspector] public PlayerStatsManager playerStatsManager;

    [Header("FLAGS")]
    [SerializeField] public bool isSprinting = false;

    // [Header("STATS")]
    // public int endurance = 1;

    protected override void Awake()
    {
        base.Awake();

        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();

        characterStatsManager = GetComponent<CharacterStatsManager>();
        // playerStatsManager = GetComponent<PlayerStatsManager>();

        // Debug.Log(characterStatsManager.CurrentStamina);
        // //+= 
        // Debug.Log(PlayerUIManager.instance.playerUIHudManager.SetNewStaminaValue);

        // THIS WILL BE MOVED WHEN SAVING AND LOADING IS ADDED
        characterStatsManager.maxStamina = characterStatsManager.CalculateStaminaBasedOnEnduranceLevel(characterStatsManager.endurance);
        characterStatsManager.CurrentStamina = characterStatsManager.CalculateStaminaBasedOnEnduranceLevel(characterStatsManager.endurance);
        PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(characterStatsManager.maxStamina);
        // Debug.Log(characterStatsManager.CurrentStamina);


    }

    protected override void Update()
    {
        base.Update();

        // HANDLE ALL MOVEMENT 
        playerLocomotionManager.HandleAllMovement();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        PlayerCamera.instance.HandleAllCameraActions();
        // Debug.Log("HandleAllCameraActions()");
    }
    
}
