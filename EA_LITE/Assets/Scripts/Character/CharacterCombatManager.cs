using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombatManager : MonoBehaviour
{
    // public WeaponItem currentWeaponBeingUsed;

    // private bool isInvulnerable = false;

    //may not need
    protected CharacterManager character;

    [Header("Attack Target")]
    public CharacterManager currentTarget;

    [Header("Attack Type")]
    public AttackType currentAttackType;

    [Header("Lock On Transform")]
    public Transform lockOnTransform;
    
    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public virtual void SetTarget(CharacterManager newTarget)
    {
        if(newTarget != null)
        {
            currentTarget = newTarget;
            // Debug.Log("currentTarget: " + currentTarget);
            //IDS?
            // character.CurrentLockOnTargetID = newTarget.GetComponent<
        }
        else
        {
            currentTarget = null;
        }
    }

    private void EnableIsInvulnerable()
    {
        character.isInvulnerable = true;
    }

    private void DisableIsInvulnerable()
    {
        character.isInvulnerable = false;
    }
}
