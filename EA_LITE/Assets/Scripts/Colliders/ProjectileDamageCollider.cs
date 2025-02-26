using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamageCollider : DamageCollider
{
    // public GameObject projectile;
    
    protected override void OnTriggerEnter(Collider other)
    {
        // gets component of the character being damaged
        PlayerManager damageTarget = other.GetComponentInParent<PlayerManager>();


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

        Destroy(gameObject);
    }
}
