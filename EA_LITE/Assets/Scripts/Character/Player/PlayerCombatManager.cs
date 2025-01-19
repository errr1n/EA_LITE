using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : CharacterCombatManager
{
    PlayerManager playerManager;
    public WeaponItem currentWeaponBeingUsed;

    protected override void Awake()
    {
        base.Awake();

        playerManager = GetComponent<PlayerManager>();
    }

    // may need to call player manager
    public void PerformWeaponBasedAction(WeaponItemAction weaponAction, WeaponItem weaponPerformingAction)
    {
        // perform the action
        weaponAction.AttemptToPerformAction(playerManager, weaponPerformingAction);
    }
}
