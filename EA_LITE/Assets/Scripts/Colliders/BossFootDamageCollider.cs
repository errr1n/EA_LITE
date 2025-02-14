using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFootDamageCollider : DamageCollider
{
    //46
    [SerializeField] AIBossCharacterManager bossCharacter;

    protected override void Awake()
    {
        base.Awake();

        damageCollider = GetComponentInChildren<Collider>();
        bossCharacter = GetComponentInParent<AIBossCharacterManager>();
    }

    protected override void DamageTarget(CharacterManager damageTarget)
    {
        // WE DO NOT WANT TO DAMAGE THE SAME TARGET MORE THAN ONCE IN A SINGLE ATTACK (MULTIPLE LIMBS -> MULTIPLE COLLIDERS)
        // SO WE ADD TO A LIST THAT CHECKS BEFORE APPLYING DAMAGE
        if(charactersDamaged.Contains(damageTarget))
        {
            return; // CAN ONLY BE HIT ONCE
        }

        charactersDamaged.Add(damageTarget); 

        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
        damageEffect.physicalDamage = physicalDamage;

        damageEffect.contactPoint = contactPoint;
        damageEffect.angleHitFrom = Vector3.SignedAngle(bossCharacter.transform.forward, damageTarget.transform.forward, Vector3.up);

        //MIGHT NEED
        // damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);

        damageTarget.ProcessCharacterDamage(
            damageTarget,
            bossCharacter,
            damageEffect.physicalDamage,
            damageEffect.angleHitFrom,
            damageEffect.contactPoint.x,
            damageEffect.contactPoint.y,
            damageEffect.contactPoint.z);
    }
}
