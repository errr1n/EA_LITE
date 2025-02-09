using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    [Header("DEBUG MENU")]
    [SerializeField] bool respawnCharacter = false;
    [SerializeField] bool SwitchRightWeapon = false;

    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
    [HideInInspector] public PlayerUIHudManager playerUIHudManager;
    [HideInInspector] public PlayerUIPopUpManager playerUIPopUpManager;
    [HideInInspector] public PlayerInventoryManager playerInventoryManager;
    [HideInInspector] public PlayerEquipmentManager playerEquipmentManager;
    [HideInInspector] public PlayerCombatManager playerCombatManager;

    public bool isUsingRightHand = false;

    public int _currentWeaponBeingUsed = 0;
    public int CurrentWeaponBeingUsed{
        get{return _currentWeaponBeingUsed;}
        set{
            playerEquipmentManager.OnCurrentWeapongBeingUsedIDChange(_currentWeaponBeingUsed, value);
            _currentWeaponBeingUsed = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        
        playerUIHudManager = GetComponent<PlayerUIHudManager>();
        playerUIPopUpManager = GetComponent<PlayerUIPopUpManager>();

        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
        playerCombatManager = GetComponent<PlayerCombatManager>();

        // THIS WILL BE MOVED WHEN SAVING AND LOADING IS ADDED

        // HEALTH
        characterStatsManager.maxHealth = characterStatsManager.CalculateHealthBasedOnVitalityLevel(characterStatsManager.currentVitality);
        characterStatsManager.CurrentHealth = characterStatsManager.CalculateHealthBasedOnVitalityLevel(characterStatsManager.currentVitality);
        PlayerUIManager.instance.playerUIHudManager.SetMaxHealthValue(characterStatsManager.maxHealth);
        
        // STAMINA
        characterStatsManager.maxStamina = characterStatsManager.CalculateStaminaBasedOnEnduranceLevel(characterStatsManager.currentEndurance);
        characterStatsManager.CurrentStamina = characterStatsManager.CalculateStaminaBasedOnEnduranceLevel(characterStatsManager.currentEndurance);
        PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(characterStatsManager.maxStamina);
    }

    protected override void Update()
    {
        base.Update();

        // HANDLE ALL MOVEMENT 
        playerLocomotionManager.HandleAllMovement();

        // REGENERATE STAMINA
        characterStatsManager.RegenerateStamina();

        PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue(characterStatsManager.CurrentHealth);
        // playerEquipmentManager.OnCurrentRightHandWeaponIDChange(playerEquipmentManager._currentRightHandWeaponID);
        // playerEquipmentManager.CurrentRightHandWeaponID = playerEquipmentManager._currentRightHandWeaponID;

        DebugMenu();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        PlayerCamera.instance.HandleAllCameraActions();
    }

    public override IEnumerator ProcessDeathEvent(bool manuallySelectDamageAnimation = false)
    {
        PlayerUIManager.instance.playerUIPopUpManager.SendYouDiedPopUp();

        return base.ProcessDeathEvent(manuallySelectDamageAnimation);

        // RESPAWN PLAYER?
    }

    public override void ReviveCharacter()
    {
        base.ReviveCharacter();

        isDead = false;
        characterStatsManager.CurrentHealth = characterStatsManager.maxHealth;
        characterStatsManager.CurrentStamina = characterStatsManager.maxStamina;

        // PLAY REBIRTH ANIMATION
        characterAnimatorManager.PlayTargetActionAnimation("Empty", true);
    }

    protected override void HandleStatUpdates()
    {
        // HEALTH
        if(characterStatsManager.currentVitality != characterStatsManager.newVitality)
        {
            // UPDATES CURRENT VITALITY TO NEW VITALTY
            characterStatsManager.currentVitality = characterStatsManager.newVitality;
            // UPDATES MAX HEALTH BASED ON VITALTY
            characterStatsManager.maxHealth = characterStatsManager.CalculateHealthBasedOnVitalityLevel(characterStatsManager.currentVitality);
            // SETS HEALTH TO FULL WHEN UPDATING MAX HEALTH 
            characterStatsManager.CurrentHealth = characterStatsManager.CalculateHealthBasedOnVitalityLevel(characterStatsManager.currentVitality);
            // DISPLAYS UPDATE ON HUD STAT BARS
            PlayerUIManager.instance.playerUIHudManager.SetMaxHealthValue(characterStatsManager.maxHealth);
        }

        // STAMINA
        if(characterStatsManager.currentEndurance != characterStatsManager.newEndurance)
        {
            characterStatsManager.currentEndurance = characterStatsManager.newEndurance;
            characterStatsManager.maxStamina = characterStatsManager.CalculateStaminaBasedOnEnduranceLevel(characterStatsManager.currentEndurance);
            characterStatsManager.CurrentStamina = characterStatsManager.CalculateStaminaBasedOnEnduranceLevel(characterStatsManager.currentEndurance);
            PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(characterStatsManager.maxStamina);
        }
    }

    // DELETE LATER
    private void DebugMenu()
    {
        if(respawnCharacter)
        {
            respawnCharacter = false;

            ReviveCharacter();
            Debug.Log("REVIVE");
        }

        if(SwitchRightWeapon)
        {
            SwitchRightWeapon = false;

            playerEquipmentManager.SwitchRightWeapon();
        }
    }

    public void SetCharacterActionHand(bool rightHandedAction)
    {
        if(rightHandedAction)
        {
            isUsingRightHand = true;
        }
    }
    
}
