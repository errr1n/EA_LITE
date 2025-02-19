using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossCombatManager : AICharacterCombatManager
{
    AITitanCharacterManager titanManager;

    [Header("Damage Colliders")]
    [SerializeField] BossHandDamageCollider bossRightHandDamageCollider;
    [SerializeField] BossFootDamageCollider bossRightFootDamageCollider;
    //left hand

    [Header("Damage")]
    [SerializeField] int baseDamage = 25;
    [SerializeField] float attack01DamageModifier = 1.0f;
    [SerializeField] float attack02DamageModifier = 1.4f;
    //can add more attacks

    //VFX - 46

    protected override void Awake()
    {
        base.Awake();

        titanManager = GetComponent<AITitanCharacterManager>();
    }

    public void SetAttack01Damage()
    {
        aiCharacter.characterSoundFXManager.PlayAttackGrunt();
        // RIGHT HAND SWAT
        bossRightHandDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
        // LEFT HAND SWAT
    }

    public void SetAttack02Damage()
    {
        aiCharacter.characterSoundFXManager.PlayAttackGrunt();
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
        //PLAY WHOOSH SOUND
        titanManager.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(titanManager.bossSoundFXManager.handWhooshes));
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
        bossRightFootDamageCollider.EnableDamageCollider();
    }

    public void CloseBossRightFootDamageCollider()
    {
        //close the right foot colldier
        bossRightFootDamageCollider.DisableDamageCollider();
    }


    public override void PivotTowardsTarget(AICharacterManager aiCharacter)
    {
        //if character is not performing an action
        if(aiCharacter.isPerformingAction)
        {
            return;
        }

        // PLAY A PIVOT ANIMATION DEPENDING ON VIEWABLE ANGLE OF CURRENT TARGET
        // turn right 90
        if(viewableAngle >= 61 && viewableAngle <= 110)
        {
            aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_90", true);
        }
        // turn left 90
        else if(viewableAngle <= -61 && viewableAngle >= -110)
        {
            aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_90", true);
        }
        // turn right 180
        if(viewableAngle >= 146 && viewableAngle <= 180)
        {
            aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_180", true);
        }
        // turn left 180
        else if(viewableAngle <= -146 && viewableAngle >= -180)
        {
            aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_180", true);
        }
    }
}
