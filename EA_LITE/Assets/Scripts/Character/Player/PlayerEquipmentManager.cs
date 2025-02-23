using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : CharacterEquipmentManager
{
    [HideInInspector] public PlayerManager player;
    [HideInInspector] public PlayerCombatManager playerCombatManager;

    [Header("Right Hand")]
    // access weapon instantiation slot
    public WeaponModelInstantiationSlot rightHandSlot;
    // access weapon information
    [SerializeField] WeaponManager rightWeaponManager;
    // weapon model
    public GameObject rightHandWeaponModel;

    // weapon ID
    public int _currentRightHandWeaponID = 0;
    public int CurrentRightHandWeaponID{
        get{return _currentRightHandWeaponID;}
        set{
            OnCurrentRightHandWeaponIDChange(_currentRightHandWeaponID, value);
            _currentRightHandWeaponID = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
        playerCombatManager = GetComponent<PlayerCombatManager>();

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
        // list of available weapon slots
        WeaponModelInstantiationSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInstantiationSlot>();

        foreach (var weaponSlot in weaponSlots)
        {
            // if the weapon slot is set to right hand (RightHand enum)
            if(weaponSlot.weaponSlot == WeaponModelSlot.RightHand)
            {
                // set this weapon slot to be the right hand
                rightHandSlot = weaponSlot;
            }

            //could add other slots here (left hand, belt, back etc.)
        }
    }

    public void LoadWeaponOnBothHands()
    {
        //load right handed weapon
        LoadRightWeapon();
    }

    // RIGHT HAND WEAPON

    public void SwitchRightWeapon()
    {
        // play equiping animation

        WeaponItem selectedWeapon = null;

        // add one to our weapon index to switch weapon
        player.playerInventoryManager.rightHandWeaponIndex += 1;
        
        // Make sure index never goes out of bounds
        if(player.playerInventoryManager.rightHandWeaponIndex < 0 || player.playerInventoryManager.rightHandWeaponIndex > 1)
        {
            // if index is greater than max # weapons -  1, reset index to 0 (back to first weapon)
            player.playerInventoryManager.rightHandWeaponIndex = 0;

            // we can check if there is more than one weapon
            float weaponCount = 0;
            WeaponItem firstWeapon = null;
            int firstWeaponPosition = 0;

            // go through the list of weapons available in the right hand slot
            for(int i = 0; i < player.playerInventoryManager.weaponsInRightHandSlot.Length; i++)
            {
                // add to weapon count
                weaponCount += 1;

                // if first weapon hasn't been assigned
                if(firstWeapon == null)
                {
                    // assign the first weapon to the chosen item from weaponsInRightHandSlot
                    firstWeapon = player.playerInventoryManager.weaponsInRightHandSlot[i];
                    firstWeaponPosition = i;
                }
            }

            // unarmed
            if(weaponCount <= 1)
            {
                // player.playerInventoryManager.rightHandWeaponIndex = -1;
                // selectedWeapon = Instantiate
            }
            else
            {
                // index is 0 (first weapon index)
                player.playerInventoryManager.rightHandWeaponIndex = firstWeaponPosition;
                // set the current right hand weapon ID to that chosen from weaponsInRightHandSlot list
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
    }

    public void LoadRightWeapon()
    {
        if(player.playerInventoryManager.currentRightHandWeapon != null)
        {
            // REMOVE THE OLD WEAPON
            rightHandSlot.UnloadWeapon();

            // BRING IN NEW WEAPON
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

    // weapon ID change on right hand
    public void OnCurrentRightHandWeaponIDChange(int oldID, int newID)
    {
        WeaponItem newWeapon = Instantiate(WorldItemDatabase.instance.GetWeaponByID(newID));
        player.playerInventoryManager.currentRightHandWeapon = newWeapon;
        player.playerEquipmentManager.LoadRightWeapon();

        PlayerUIManager.instance.playerUIHudManager.SwapWeaponIcon(newID);
    }

    // weapon being used ID change
    public void OnCurrentWeapongBeingUsedIDChange(int oldID, int newID)
    {
        WeaponItem newWeapon = Instantiate(WorldItemDatabase.instance.GetWeaponByID(newID));
        player.playerCombatManager.currentWeaponBeingUsed = newWeapon;
    }

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
