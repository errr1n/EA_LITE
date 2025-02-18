using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Dynamite Action")]

public class DynamiteWeaponItemAction : WeaponItemAction
{
    // ANIMATION STRING
    // [SerializeField] string Place_Dynamite = "Place_Dynamite";

    public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

        // check for stops 
        // stamina
        //if jumping
        //if dodging
        if(!playerPerformingAction.isPerformingAction)
        {
            PlaceDynamite(playerPerformingAction, weaponPerformingAction);
        }
        // PerformLightAttack(playerPerformingAction, weaponPerformingAction);
    }

    private void PlaceDynamite(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        if(playerPerformingAction.isUsingRightHand)
        {
            // PLAY ANIMATION
            // playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack, Light_Attack, true);
            Debug.Log("PLACE DYNAMITE");
        }
    }
}
