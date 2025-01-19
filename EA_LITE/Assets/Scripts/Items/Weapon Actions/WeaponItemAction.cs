using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Test  Action")]

public class WeaponItemAction : ScriptableObject
{
    public int actionID;

    public virtual void AttemptToPerformAction(PlayerEquipmentManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        // playerPerformingAction.playerCombatManager.currentWeaponBeingUsed = weaponPerformingAction;
        playerPerformingAction.CurrentWeaponBeingUsed = weaponPerformingAction.itemID;
        Debug.Log("The action has fired");
        Debug.Log("weaponPerformingAction.itemID: " + weaponPerformingAction.itemID);
    }
}
