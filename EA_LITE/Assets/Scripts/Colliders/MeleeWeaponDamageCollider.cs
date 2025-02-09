using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponDamageCollider : DamageCollider
{
    [Header("Attacking Character")]
    public CharacterManager characterCausingDamage; // can check effects

    // [Header("WEAPON ATTACK MODIFIERS")]

    public CharacterManager setDamageTarget;

    protected override void Awake()
    {
        base.Awake();

        if(damageCollider == null)
        {
            damageCollider = GetComponent<Collider>();
        }
        damageCollider.enabled = false; // melee weapon colliders should be disabled at start, only enabled when animations allow it
    }

    protected override void OnTriggerEnter(Collider other)
    {
        CharacterManager damageTarget = other.GetComponentInParent<CharacterManager>();
        setDamageTarget = damageTarget;

        if(damageTarget != null)
        {
            // we do not want to damage ourselves
            if(damageTarget == characterCausingDamage)
            {
                return;
            }

            contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

            // CHECK IF WE CAN DAMAGE THIS TARGET (BLOCKING)

            // CHECK IF INVULNERABLE (DODGE)

            //DAMAGE
            DamageTarget(damageTarget);
            // Debug.Log("HERE");
        }
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
        damageEffect.angleHitFrom = Vector3.SignedAngle(characterCausingDamage.transform.forward, damageTarget.transform.forward, Vector3.up);

        //MIGHT NEED
        // damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);

        damageTarget.ProcessCharacterDamage(
            damageTarget,
            characterCausingDamage,
            damageEffect.physicalDamage,
            damageEffect.angleHitFrom,
            damageEffect.contactPoint.x,
            damageEffect.contactPoint.y,
            damageEffect.contactPoint.z);
    }

    // apply damage modifiers
}
