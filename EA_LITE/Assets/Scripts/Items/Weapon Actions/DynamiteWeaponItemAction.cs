using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Dynamite Action")]

public class DynamiteWeaponItemAction : WeaponItemAction
{
    // ANIMATION STRING
    [SerializeField] string Place_Dynamite = "Place_Dynamite";
    [SerializeField] GameObject dynamiteObject;
    [SerializeField] public GameObject instantiatedGameObject;

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

    // MIGHT MOVE TO DYNAMITE SCRIPT -> ACCESS INSTANTIATED OBJECT POSTION
    private void PlaceDynamite(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
    {
        if(playerPerformingAction.isUsingRightHand)
        {
            // PLAY ANIMATION
            playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack, Place_Dynamite, true);

            //instantiate the dynamite game object
            instantiatedGameObject = Instantiate(dynamiteObject);
            // sets the transform of the dynamite
            instantiatedGameObject.transform.position = playerPerformingAction.transform.position;
        }
    }
}
