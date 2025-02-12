using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossCombatManager : AICharacterCombatManager
{
    [Header("Damage Colliders")]
    [SerializeField] BossHandDamageCollider bossRightHandDamageCollider;
    // [SerializeField] BossHandDamageCollider bossLeftHandDamageCollider;
    [SerializeField] BossFootDamageCollider bossRightFootDamageCollider;
    //left hand

    [Header("Damage")]
    [SerializeField] int baseDamage = 25;
    [SerializeField] float attack01DamageModifier = 1.0f;
    [SerializeField] float attack02DamageModifier = 1.4f;
    //add more attacks

    public void SetAttack01Damage()
    {
        // RIGHT HAND SWAT
        bossRightHandDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
        // LEFT HAND SWAT
    }

    public void SetAttack02Damage()
    {
        // RIGHT FOOT STOMP
        bossRightFootDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
        // LEFT FOOT STOMP

    }

    // RIGHT HAND DAMAGE COLLIDER
    public void OpenBossRightHandDamageColider()
    {
        //play attack sound
        // aiCharacter.characterSoundFXManager.PlayAttackGrunt();
        //open the right hand colldier
        bossRightHandDamageCollider.EnableDamageCollider();
    }

    public void CloseBossRightHandDamageColider()
    {
        bossRightHandDamageCollider.DisableDamageCollider();
    }

    // RIGHT FOOT DAMAGE COLLIDER
    public void OpenBossRightFootDamageCollider()
    {
        //play attack sound
        // aiCharacter.characterSoundFXManager.PlayAttackGrunt();
        //open the right foot colldier
        // Debug.Log("open");
        bossRightFootDamageCollider.EnableDamageCollider();
    }

    public void CloseBossRightFootDamageCollider()
    {
        bossRightFootDamageCollider.DisableDamageCollider();
    }

    public override void PivotTowardsTarget(AICharacterManager aiCharacter)
    {
        // PLAY A PIVOT ANIMATION DEPENDING ON VIEWABLE ANGLE OF CURRENT TARGET
        if(aiCharacter.isPerformingAction)
        {
            return;
        }

        if(viewableAngle >= 61 && viewableAngle <= 110)
        {
            aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_90", true);
        }

        else if(viewableAngle <= -61 && viewableAngle >= -110)
        {
            aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_90", true);
        }

        if(viewableAngle >= 146 && viewableAngle <= 180)
        {
            aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_180", true);
        }

        else if(viewableAngle <= -146 && viewableAngle >= -180)
        {
            aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_180", true);
        }
    }
}
