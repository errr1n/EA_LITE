using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Light Attack Action")]

// PICKAXE ACTION (what happens when the pickaxe is in hand)
public class LightAttackWeaponItemAction : WeaponItemAction
{
    // string for attack animation
    [SerializeField] string Light_Attack = "Light_Attack";

    public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);
        
        // if player is not already performing an action
        if(!playerPerformingAction.isPerformingAction)
        {
            // perform attack
            PerformLightAttack(playerPerformingAction, weaponPerformingAction);
        }
    }

    private void PerformLightAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        // check which hand the player is using
        if(playerPerformingAction.isUsingRightHand)
        {
            // PLAY ANIMATION
            playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack, Light_Attack, true);
        }
    }
}
