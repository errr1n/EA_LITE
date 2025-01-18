using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : CharacterEquipmentManager
{
    // public WeaponItem currentWeapon;
    PlayerManager player;

    public WeaponModelInstantiationSlot rightHandSlot;

    public GameObject rightHandWeaponModel;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();

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

    public void LoadRightWeapon()
    {
        if(player.playerInventoryManager.currentRightHandWeapon != null)
        {
            rightHandWeaponModel = Instantiate(player.playerInventoryManager.currentRightHandWeapon.weaponModel);
            rightHandSlot.LoadWeapon(rightHandWeaponModel);
        }
    }

    //can load a left hand weapon
}
