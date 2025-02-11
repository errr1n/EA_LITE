using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandDamageCollider : DamageCollider
{
    [SerializeField] AICharacterManager enemyCharacter;

    protected override void Awake()
    {
        base.Awake();

        damageCollider = GetComponentInChildren<Collider>();
        enemyCharacter = GetComponentInParent<AICharacterManager>();
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
        damageEffect.angleHitFrom = Vector3.SignedAngle(enemyCharacter.transform.forward, damageTarget.transform.forward, Vector3.up);

        //MIGHT NEED
        // damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);

        damageTarget.ProcessCharacterDamage(
            damageTarget,
            enemyCharacter,
            damageEffect.physicalDamage,
            damageEffect.angleHitFrom,
            damageEffect.contactPoint.x,
            damageEffect.contactPoint.y,
            damageEffect.contactPoint.z);
    }
}
