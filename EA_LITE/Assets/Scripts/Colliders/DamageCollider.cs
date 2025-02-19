using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [Header("Collider")]
    [SerializeField] protected Collider damageCollider; // reference box collider on weapon

    [Header("Damage")]
    public float physicalDamage = 0; // damage dealt by collider

    [Header("Contact Point")]
    protected Vector3 contactPoint; // point where collider meets another collider

    [Header("Characters Damaged")]
    // list of characters damaged by collider
    protected List<CharacterManager> charactersDamaged = new List<CharacterManager>();

    protected virtual void Awake()
    {
        //
    }
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        // gets component of the character being damaged
        CharacterManager damageTarget = other.GetComponentInParent<CharacterManager>();


        if(damageTarget != null)
        {
            // point where colliders touched
            contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

            // CHECK IF WE CAN DAMAGE THIS TARGET (BLOCKING)

            // CHECK IF INVULNERABLE (DODGE)
            // if(damageTarget.isInvulnerable)
            // {
            //     return;
            // }

            //DAMAGE the character
            DamageTarget(damageTarget);

            //PRINT WHAT COLLIDER IS HIT
            // Debug.Log(other);
        }
    }

    //apply damage to the character that has been hit
    protected virtual void DamageTarget(CharacterManager damageTarget)
    {
        // WE DO NOT WANT TO DAMAGE THE SAME TARGET MORE THAN ONCE IN A SINGLE ATTACK (MULTIPLE LIMBS -> MULTIPLE COLLIDERS)
        // SO WE ADD TO A LIST THAT CHECKS BEFORE APPLYING DAMAGE
        if(charactersDamaged.Contains(damageTarget))
        {
            return; // CAN ONLY BE HIT ONCE
        }

        // add the character who's collider has been hit to charactersDamaged list
        charactersDamaged.Add(damageTarget); 

        // instantiate damage effect scriptable object
        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
        damageEffect.physicalDamage = physicalDamage;

        // track the collider contact point
        damageEffect.contactPoint = contactPoint;

        // apply physical damage effect
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
