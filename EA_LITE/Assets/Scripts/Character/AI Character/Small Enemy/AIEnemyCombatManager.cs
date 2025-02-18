using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyCombatManager : AICharacterCombatManager
{
    [Header("Damage Colliders")]
    // get collider
    [SerializeField] EnemyHandDamageCollider rightHandDamageCollider;
    //left hand

    [Header("Damage")]
    // attack base damage
    [SerializeField] int baseDamage = 25;
    // damage modifiers for different attacks
    [SerializeField] float attack01DamageModifier = 1.0f;
    [SerializeField] float attack02DamageModifier = 1.4f;

    public void SetAttack01Damage()
    {
        // set the physical damage of the collider
        rightHandDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
        //left
    }

    public void SetAttack02Damage()
    {
        // set the physical damage of the collider
        rightHandDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
        //left
    }

    public void OpenRightHandDamageColider()
    {
        //play attack sound
        aiCharacter.characterSoundFXManager.PlayAttackGrunt();
        //open the right hand colldier
        rightHandDamageCollider.EnableDamageCollider();
    }

    public void CloseRightHandDamageColider()
    {
        // close the right hand collider
        rightHandDamageCollider.DisableDamageCollider();
    }

    //left
}
