using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : CharacterEquipmentManager
{
    // public WeaponItem currentWeapon;
    [HideInInspector] public PlayerManager player;
    [HideInInspector] public PlayerCombatManager playerCombatManager;
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;

    public WeaponModelInstantiationSlot rightHandSlot;

    [SerializeField] WeaponManager rightWeaponManager;

    public GameObject rightHandWeaponModel;

    public int _currentRightHandWeaponID = 0;
    public int CurrentRightHandWeaponID{
        get{return _currentRightHandWeaponID;}
        set{
            // UPDATES HEALTH UI BAR WHEN HEALTH CHANGES 
            OnCurrentRightHandWeaponIDChange(_currentRightHandWeaponID, value);
            _currentRightHandWeaponID = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
        playerCombatManager = GetComponent<PlayerCombatManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();

        //GET OUR SLOTS
        InitializeWeaponSlots();
    }

    protected override void Start()
    {
        base.Start();

        LoadWeaponOnBothHands();
    }

    private void InitializeWeaponSlots()
    {
        WeaponModelInstantiationSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInstantiationSlot>();

        foreach (var weaponSlot in weaponSlots)
        {
            if(weaponSlot.weaponSlot == WeaponModelSlot.RightHand)
            {
                rightHandSlot = weaponSlot;
            }
            //could add other slots here (left hand, belt, back etc.)
        }
    }

    public void LoadWeaponOnBothHands()
    {
        LoadRightWeapon();
    }

    // RIGHT HAND WEAPON

    public void SwitchRightWeapon()
    {
        // play equiping animation

        WeaponItem selectedWeapon = null;

        // ad one to our weapon index to switch to weapon
        player.playerInventoryManager.rightHandWeaponIndex += 1;


        // Make sure index never goes out of bounds
        if(player.playerInventoryManager.rightHandWeaponIndex < 0 || player.playerInventoryManager.rightHandWeaponIndex > 1)
        {
            player.playerInventoryManager.rightHandWeaponIndex = 0;

            // we can check if there is more than one weapon
            float weaponCount = 0;
            WeaponItem firstWeapon = null;
            int firstWeaponPosition = 0;

            for(int i = 0; i < player.playerInventoryManager.weaponsInRightHandSlot.Length; i++)
            {
                weaponCount += 1;

                if(firstWeapon == null)
                {
                    firstWeapon = player.playerInventoryManager.weaponsInRightHandSlot[i];
                    firstWeaponPosition = i;
                }
            }

            if(weaponCount <= 1)
            {
                // player.playerInventoryManager.rightHandWeaponIndex = -1;
                // selectedWeapon = Instantiate
            }
            else
            {
                player.playerInventoryManager.rightHandWeaponIndex = firstWeaponPosition;
                CurrentRightHandWeaponID = firstWeapon.itemID;
            }

            return;
        }

        foreach (WeaponItem weapon in player.playerInventoryManager.weaponsInRightHandSlot)
        {
            // CHECK TO SEE IF THIS IS NOT THE UNARMED WEAPON
            selectedWeapon = player.playerInventoryManager.weaponsInRightHandSlot[player.playerInventoryManager.rightHandWeaponIndex];
            CurrentRightHandWeaponID = player.playerInventoryManager.weaponsInRightHandSlot[player.playerInventoryManager.rightHandWeaponIndex].itemID;
            return;
        }

        if(selectedWeapon == null && player.playerInventoryManager.rightHandWeaponIndex <= 1)
        {
            SwitchRightWeapon();
        }
        // else
        // {
            // // we can check if there is more than one weapon
            // float weaponCount = 0;
            // WeaponItem firstWeapon = null;
            // // Debug.Log("firstWeapon: " + firstWeapon);
            // int firstWeaponPosition = 0;

            // for(int i = 0; i < player.playerInventoryManager.weaponsInRightHandSlot.Length; i++)
            // {
            //     // if(player.playerInventoryManager.weaponsInRightHandSlot[i].itemID != WorldItemDatabase.instance.unarmed.itemID)
            //     // {
            //         weaponCount += 1;
            //         // Debug.Log("weapon count: " + weaponCount);

            //         if(firstWeapon == null)
            //         {
            //             firstWeapon = player.playerInventoryManager.weaponsInRightHandSlot[i];
            //             firstWeaponPosition = i;
            //             // Debug.Log("firstWeapon: " + firstWeapon);
            //             // Debug.Log("firstWeaponPosition: " + firstWeaponPosition);
            //         }
            //     // }
            // }

            // if(weaponCount <= 1)
            // {
            //     // player.playerInventoryManager.rightHandWeaponIndex = -1;
            //     // selectedWeapon = Instantiate
            // }
            // else
            // {
            //     player.playerInventoryManager.rightHandWeaponIndex = firstWeaponPosition;
            //     CurrentRightHandWeaponID = firstWeapon.itemID;
            // }

            // player.playerInventoryManager.rightHandWeaponIndex = -1;
            // _currentRightHandWeaponID = firstWeapon.itemID;
        // }
    }

    public void LoadRightWeapon()
    {
        if(player.playerInventoryManager.currentRightHandWeapon != null)
        {
            // remove the old weapon
            rightHandSlot.UnloadWeapon();

            //bring in new weapon
            // spawns the weapon model from gameobject  
            rightHandWeaponModel = Instantiate(player.playerInventoryManager.currentRightHandWeapon.weaponModel);
            // load the corresponding weapon model to the right hand slot
            rightHandSlot.LoadWeapon(rightHandWeaponModel);
            // variable to access weapon manager component
            rightWeaponManager = rightHandWeaponModel.GetComponent<WeaponManager>();            
            // ASSIGN WEAPONS DAMAGE, TO ITS COLLIDER
            rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
        }
    }

    //can load a left hand weapon

    public void OnCurrentRightHandWeaponIDChange(int oldID, int newID)
    {
        WeaponItem newWeapon = Instantiate(WorldItemDatabase.instance.GetWeaponByID(newID));
        player.playerInventoryManager.currentRightHandWeapon = newWeapon;
        player.playerEquipmentManager.LoadRightWeapon();
    }

    public void OnCurrentWeapongBeingUsedIDChange(int oldID, int newID)
    {
        WeaponItem newWeapon = Instantiate(WorldItemDatabase.instance.GetWeaponByID(newID));
        player.playerCombatManager.currentWeaponBeingUsed = newWeapon;
    }

    // public void SetCharacterActionHand(bool rightHandedAction)
    // {
    //     if(rightHandedAction)
    //     {
    //         player.isUsingRightHand = true;
    //     }
    // }

    // DAMAGE COLLIDERS

    // CALLED ON ANIMATION
    public void OpenDamageCollider()
    {
        // OPEN RIGHT HAND WEAPON DAMAGE COLLIDER
        if(player.isUsingRightHand)
        {
            rightWeaponManager.meleeDamageCollider.EnableDamageCollider();
            // PLAY WHOOSH SOUND
            player.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(player.playerInventoryManager.currentRightHandWeapon.whooshes));
        }
        //left hand
    }
    
    // CALLED ON ANIMATION
    public void CloseDamageCollider()
    {
        // OPEN RIGHT HAND WEAPON DAMAGE COLLIDER
        if(player.isUsingRightHand)
        {
            rightWeaponManager.meleeDamageCollider.DisableDamageCollider();
        }
        //left hand
    }
}
