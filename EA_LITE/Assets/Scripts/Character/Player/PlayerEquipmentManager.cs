using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : CharacterEquipmentManager
{
    // public WeaponItem currentWeapon;
    PlayerManager player;

    public WeaponModelInstantiationSlot rightHandSlot;

    [SerializeField] WeaponManager rightWeaponManager;

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
}
