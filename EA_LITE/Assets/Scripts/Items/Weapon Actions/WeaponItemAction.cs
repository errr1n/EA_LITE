using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Test Action")]

public class WeaponItemAction : ScriptableObject
{
    public int actionID;

    public virtual void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        // gets the current weapon being used by checking what the ID is
        playerPerformingAction.CurrentWeaponBeingUsed = weaponPerformingAction.itemID;
    }
}
