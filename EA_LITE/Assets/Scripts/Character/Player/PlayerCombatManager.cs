using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : CharacterCombatManager
{
    PlayerManager player;

    public WeaponItem currentWeaponBeingUsed;
    // public AttackType currentAttackType;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    // may need to call player manager
    public void PerformWeaponBasedAction(WeaponItemAction weaponAction, WeaponItem weaponPerformingAction)
    {
        // perform the action
        weaponAction.AttemptToPerformAction(player, weaponPerformingAction);

        // weaponAction.AttemptToPerformAction(weaponAction.actionID, weaponPerformingAction.itemID);
    }

    // drain stamina?
    // switch statement for attack type stamina drain
}
