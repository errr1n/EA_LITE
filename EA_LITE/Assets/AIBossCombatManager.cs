using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossCombatManager : AICharacterCombatManager
{
    [Header("Damage Colliders")]
    [SerializeField] BossHandDamageCollider bossRightHandDamageCollider;
    //left hand

    [Header("Damage")]
    [SerializeField] int baseDamage = 25;
    [SerializeField] float attack01DamageModifier = 1.0f;
    [SerializeField] float attack02DamageModifier = 1.4f;
    //add more attacks

    public void SetAttack01Damage()
    {
        bossRightHandDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
        //left
    }

    public void SetAttack02Damage()
    {
        bossRightHandDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
        //left
    }

    public void OpenBossRightHandDamageColider()
    {
        //play attack sound
        aiCharacter.characterSoundFXManager.PlayAttackGrunt();
        //open the right hand colldier
        bossRightHandDamageCollider.EnableDamageCollider();
    }

    public void CloseBossRightHandDamageColider()
    {
        bossRightHandDamageCollider.DisableDamageCollider();
    }
}
