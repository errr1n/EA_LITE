using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemBasedAction : ScriptableObject
{
    public int actionID;

    public virtual void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        // WHAT EVERY WEAPON HAS IN COMMON
    }
}
