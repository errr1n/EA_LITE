using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombatManager : MonoBehaviour
{
    //may not need
    CharacterManager character;

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
        // if(character)

        if(newTarget != null)
        {
            currentTarget = newTarget;
            Debug.Log("currentTarget: " + currentTarget);
            //IDS?
            // character.CurrentLockOnTargetID = newTarget.GetComponent<
        }
        else
        {
            currentTarget = null;
        }
    }
}
