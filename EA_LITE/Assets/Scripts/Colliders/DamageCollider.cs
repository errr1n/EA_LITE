using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [Header("Collider")]
    protected Collider damageCollider;

    [Header("Damage")]
    public float physicalDamage = 0;

    [Header("Contact Point")]
    protected Vector3 contactPoint;

    [Header("Characters Damaged")]
    protected List<CharacterManager> charactersDamaged = new List<CharacterManager>();
    
    private void OnTriggerEnter(Collider other)
    {
        CharacterManager damageTarget = other.GetComponentInParent<CharacterManager>();


        if(damageTarget != null)
        {
            contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

            // CHECK IF WE CAN DAMAGE THIS TARGET (BLOCKING)

            // CHECK IF INVULNERABLE (DODGE)

            //DAMAGE
            DamageTarget(damageTarget);
            Debug.Log(other);
        }
    }

    protected virtual void DamageTarget(CharacterManager damageTarget)
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

        damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);
    }

    public virtual void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public virtual void DisableDamageCollider()
    {
        damageCollider.enabled = false;
        charactersDamaged.Clear(); // WE RESET THE CHARCTERS THAT HAVE BEEN HIT WHEN WE RESET THE COLLIDER, SO THEY MAY BE HIT AGAIN
    }
}
