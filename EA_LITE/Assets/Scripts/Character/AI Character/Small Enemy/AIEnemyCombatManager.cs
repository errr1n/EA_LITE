using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyCombatManager : AICharacterCombatManager
{
    [Header("Damage Colliders")]
    [SerializeField] EnemyHandDamageCollider rightHandDamageCollider;
    //left hand

    [Header("Damage")]
    [SerializeField] int baseDamage = 25;
    [SerializeField] float attack01DamageModifier = 1.0f;
    [SerializeField] float attack02DamageModifier = 1.4f;

    public void SetAttack01Damage()
    {
        rightHandDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
        //left
    }

    public void SetAttack02Damage()
    {
        rightHandDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
        //left
    }

    public void OpenRightHandDamageColider()
    {
        rightHandDamageCollider.EnableDamageCollider();
    }

    public void CloseRightHandDamageColider()
    {
        rightHandDamageCollider.DisableDamageCollider();
    }

    //left
}
