using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossCombatManager : AICharacterCombatManager
{
    AITitanCharacterManager titanManager;

    [Header("Damage Colliders")]
    [SerializeField] BossHandDamageCollider bossRightHandDamageCollider;
    // [SerializeField] BossHandDamageCollider bossLeftHandDamageCollider;
    [SerializeField] BossFootDamageCollider bossRightFootDamageCollider;
    // [SerializeField] Transform bossRightStompFoot;
    // [SerializeField] Transform bossLeftStompFoot;
    // [SerializeField] float stompAttackAOERadius = 1.5f;
    //left hand

    [Header("Damage")]
    [SerializeField] int baseDamage = 25;
    [SerializeField] float attack01DamageModifier = 1.0f;
    [SerializeField] float attack02DamageModifier = 1.4f;
    //add more attacks

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

    // RIGHT STOMP
    // public void ActivateBossRightStomp()
    // {
    //     Collider[] colliders = Physics.OverlapSphere(bossRightStompFoot.position, stompAttackAOERadius, WorldUtilityManager.instance.GetCharacterLayers());
    //     List<CharacterManager> charactersDamaged = new List<CharacterManager>();

    //     foreach(var collider in colliders)
    //     {
    //         CharacterManager character = collider.GetComponentInParent<CharacterManager>();

    //         if(character != null)
    //         {
    //             if(charactersDamaged.Contains(character))
    //             {
    //                 continue;
    //             }  

    //             charactersDamaged.Add(character);

    //             //check for block
    //             TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
    //             damageEffect.physicalDamage = stompDamage;

    //             // damageEffect.contactPoint = contactPoint;

    //             character.characterEffectsManager.ProcessInstantEffect(damageEffect);
    //         }
    //     }
    // }

    // LEFT STOMP
    // public void ActivateBossLefttStomp()
    // {
    //     Collider[] colliders = Physics.OverlapSphere(bossLeftStompFoot.position, stompAttackAOERadius, WorldUtilityManager.instance.GetCharacterLayers());
    //     List<CharacterManager> charactersDamaged = new List<CharacterManager>();

    //     foreach(var collider in colliders)
    //     {
    //         CharacterManager character = collider.GetComponentInParent<CharacterManager>();

    //         if(character != null)
    //         {
    //             if(charactersDamaged.Contains(character))
    //             {
    //                 continue;
    //             }  

    //             charactersDamaged.Add(character);

    //             //check for block
    //             TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
    //             damageEffect.physicalDamage = stompDamage;

    //             // damageEffect.contactPoint = contactPoint;

    //             character.characterEffectsManager.ProcessInstantEffect(damageEffect);
    //         }
    //     }
    // }

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
