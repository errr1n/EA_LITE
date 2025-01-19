using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName "")]

public class LightAttackWeaponItemAction : WeaponItemAction
{
    // CharacterManager characterManager;

    // protected virtual void Awake()
    // {
    //     characterManager = GetComponent<CharacterManager>();
    // }

    public override void AttemptToPerformAction(PlayerEquipmentManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

        // check for stopping the action (STAMINA)

        // playerPerformingAction.GetComponentsInParent

        if(!playerPerformingAction.player.isGrounded)
        {
            return;
        }
    }
}
