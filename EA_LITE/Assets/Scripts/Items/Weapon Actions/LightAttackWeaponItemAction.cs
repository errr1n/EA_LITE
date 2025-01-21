using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Light Attack Action")]

public class LightAttackWeaponItemAction : WeaponItemAction
{
    [SerializeField] string Light_Attack = "Light_Attack";

    public override void AttemptToPerformAction(PlayerEquipmentManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

        // check for stops 
        // stamina
        //if jumping
        //if dodging
        if(!playerPerformingAction.player.isPerformingAction)
        {
            PerformLightAttack(playerPerformingAction, weaponPerformingAction);
        }
        // PerformLightAttack(playerPerformingAction, weaponPerformingAction);
    }

    private void PerformLightAttack(PlayerEquipmentManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        if(playerPerformingAction.isUsingRightHand)
        {
            // PLAY ANIMATION
            playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack, Light_Attack, true);
        }
    }
}
