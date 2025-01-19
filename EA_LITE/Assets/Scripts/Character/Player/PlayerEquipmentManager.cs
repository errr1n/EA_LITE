using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : CharacterEquipmentManager
{
    // public WeaponItem currentWeapon;
    PlayerManager player;
    [HideInInspector] public PlayerCombatManager playerCombatManager;

    public WeaponModelInstantiationSlot rightHandSlot;

    [SerializeField] WeaponManager rightWeaponManager;

    public GameObject rightHandWeaponModel;

    //maybe
    public int _currentWeaponBeingUsed = 0;
    public int CurrentWeaponBeingUsed{
        get{return _currentWeaponBeingUsed;}
        set{
            // UPDATES HEALTH UI BAR WHEN HEALTH CHANGES 
            OnCurrentWeapongBeingUsedIDChange(_currentWeaponBeingUsed, value);
            // Debug.Log("---VALUE---: " + value);
            _currentWeaponBeingUsed = value;
            // Debug.Log("CURRENT HEALTH: " + _currentRightHandWeaponID);
        }
    }

    public int _currentRightHandWeaponID = 0;
    public int CurrentRightHandWeaponID{
        get{return _currentRightHandWeaponID;}
        set{
            // UPDATES HEALTH UI BAR WHEN HEALTH CHANGES 
            OnCurrentRightHandWeaponIDChange(_currentRightHandWeaponID, value);
            // Debug.Log("---VALUE---: " + value);
            _currentRightHandWeaponID = value;
            // Debug.Log("CURRENT HEALTH: " + _currentRightHandWeaponID);
        }
    }

    public bool isUsingRightHand = false;

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
        // Debug.Log(player.playerInventoryManager.rightHandWeaponIndex);


        // Make sure index never goes out of bounds
        if(player.playerInventoryManager.rightHandWeaponIndex < 0 || player.playerInventoryManager.rightHandWeaponIndex > 1)
        {
            player.playerInventoryManager.rightHandWeaponIndex = 0;

            // we can check if there is more than one weapon
            float weaponCount = 0;
            WeaponItem firstWeapon = null;
            // Debug.Log("firstWeapon: " + firstWeapon);
            int firstWeaponPosition = 0;

            for(int i = 0; i < player.playerInventoryManager.weaponsInRightHandSlot.Length; i++)
            {
                // if(player.playerInventoryManager.weaponsInRightHandSlot[i].itemID != WorldItemDatabase.instance.unarmed.itemID)
                // {
                    weaponCount += 1;
                    // Debug.Log("weapon count: " + weaponCount);

                    if(firstWeapon == null)
                    {
                        firstWeapon = player.playerInventoryManager.weaponsInRightHandSlot[i];
                        firstWeaponPosition = i;
                        // Debug.Log("firstWeapon: " + firstWeapon);
                        // Debug.Log("firstWeaponPosition: " + firstWeaponPosition);
                    }
                // }
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
            // if()
            selectedWeapon = player.playerInventoryManager.weaponsInRightHandSlot[player.playerInventoryManager.rightHandWeaponIndex];
            // Debug.Log("Selected weapon: " + selectedWeapon);
            CurrentRightHandWeaponID = player.playerInventoryManager.weaponsInRightHandSlot[player.playerInventoryManager.rightHandWeaponIndex].itemID;
            // Debug.Log("current weapon ID: " + CurrentRightHandWeaponID);
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

        // if()
    }

    //can load a left hand weapon

    public void OnCurrentRightHandWeaponIDChange(int oldID, int newID)
    {
        // Debug.Log(oldID);
        // Debug.Log(newID);
        WeaponItem newWeapon = Instantiate(WorldItemDatabase.instance.GetWeaponByID(newID));
        player.playerInventoryManager.currentRightHandWeapon = newWeapon;
        player.playerEquipmentManager.LoadRightWeapon();
    }

    public void OnCurrentWeapongBeingUsedIDChange(int oldID, int newID)
    {
        // Debug.Log(oldID);
        // Debug.Log(newID);
        WeaponItem newWeapon = Instantiate(WorldItemDatabase.instance.GetWeaponByID(newID));
        playerCombatManager.currentWeaponBeingUsed = newWeapon;
    }

    public void SetCharacterActionHand(bool rightHandedAction)
    {
        if(rightHandedAction)
        {
            isUsingRightHand = true;
        }
    }
}
