using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Dynamite Action")]

// DYNAMITE ACTION (what happens when the dynamite is in hand)
public class DynamiteWeaponItemAction : WeaponItemAction
{
    [SerializeField] string Place_Dynamite = "Place_Dynamite";  // holds the assigned animation
    [SerializeField] GameObject dynamiteObject;  // holds the dynamite gameobject

    public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

        if(!playerPerformingAction.isPerformingAction)
        {
            PlaceDynamite(playerPerformingAction, weaponPerformingAction);
        }
    }

    private void PlaceDynamite(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        if(playerPerformingAction.isUsingRightHand)
        {
            // PLAY ANIMATION
            playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack, Place_Dynamite, true);
            
            //instantiate the dynamite game object
            GameObject clonedDynamite = Instantiate(dynamiteObject);
            // sets the transform of the dynamite
            clonedDynamite.transform.position = playerPerformingAction.transform.position;
        }
    }
}
