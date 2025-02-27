using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    [Header("DEBUG MENU")]
    [SerializeField] bool respawnCharacter = false;
    [SerializeField] public GameObject respawnPoint;

    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
    [HideInInspector] public PlayerUIHudManager playerUIHudManager;
    [HideInInspector] public PlayerUIPopUpManager playerUIPopUpManager;
    [HideInInspector] public PlayerInventoryManager playerInventoryManager;
    [HideInInspector] public PlayerEquipmentManager playerEquipmentManager;
    [HideInInspector] public PlayerCombatManager playerCombatManager;

    public bool isUsingRightHand = false;

    // ID of current weapon being used 
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

        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
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

        // StartCoroutine(PlayerUIManager.instance.playerUIPopUpManager.WASDTutorialPopUp(PlayerUIManager.instance.playerUIPopUpManager.WASDPopUpGameObject));
    }

    protected override void Update()
    {
        base.Update();

        // HANDLE ALL MOVEMENT 
        playerLocomotionManager.HandleAllMovement();

        // REGENERATE STAMINA
        characterStatsManager.RegenerateStamina();

        // UPDATE UI HEALTH BAR ACCORDING TO CURRENT HEALTH STATS
        PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue(characterStatsManager.CurrentHealth);

        // Debug.Log(respawnCharacter);

        DebugMenu();

        if(isDead)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("respawn");
                respawnCharacter = true;
            }
        }
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        PlayerCamera.instance.HandleAllCameraActions();
    }

    public override IEnumerator ProcessDeathEvent(bool manuallySelectDamageAnimation = false)
    {
        PlayerUIManager.instance.playerUIPopUpManager.SendYouDiedPopUp();

        yield return base.ProcessDeathEvent(manuallySelectDamageAnimation);

        // RESPAWN PLAYER?
        // ReviveCharacter();
        // respawnCharacter = true;
    }

    public override void ReviveCharacter()
    {
        base.ReviveCharacter();

        isDead = false;
        characterStatsManager.CurrentHealth = characterStatsManager.maxHealth;
        characterStatsManager.CurrentStamina = characterStatsManager.maxStamina;

        // PLAY REBIRTH ANIMATION
        characterAnimatorManager.PlayTargetActionAnimation("Empty", true);

        // move character
        this.transform.position = respawnPoint.transform.position;
        // Debug.Log(this);
        // Debug.Log(this.transform.position);
        // Debug.Log(respawnPoint.transform.position);
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
