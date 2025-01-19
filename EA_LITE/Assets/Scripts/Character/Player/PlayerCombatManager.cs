using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : CharacterCombatManager
{
    PlayerEquipmentManager playerEquipmentManager;
    public WeaponItem currentWeaponBeingUsed;

    protected override void Awake()
    {
        base.Awake();

        playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
    }

    // may need to call player manager
    public void PerformWeaponBasedAction(WeaponItemAction weaponAction, WeaponItem weaponPerformingAction)
    {
        // perform the action
        weaponAction.AttemptToPerformAction(playerEquipmentManager, weaponPerformingAction);

        // weaponAction.AttemptToPerformAction(weaponAction.actionID, weaponPerformingAction.itemID);
    }
}
