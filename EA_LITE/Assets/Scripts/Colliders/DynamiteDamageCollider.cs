using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteDamageCollider : DamageCollider
{
    // time before dynamite explodes
    [SerializeField] private float timeBeforeExplosion = 3f;
    // explosion radius
    [SerializeField] private float damageRadius = 4f;

    // explosion particles assigned in inspector
    [SerializeField] GameObject explosionParticle;

    void Update()
    {
        StartCoroutine(CheckIfDamageable());
    }

    // function which checks if a character is within the damage sphere radius
    public IEnumerator CheckIfDamageable()
    {
        // explosion countdown
        yield return new WaitForSeconds(timeBeforeExplosion);

        // dynamitePosition = dynamiteItem.instantiatedGameObject.transform.position;

        //creates a list of colliders and creates an overlap sphere (sphere collider) of the size defined by damageRadius
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);

        foreach(Collider collider in colliders)
        {
            //check if colider is attached to a character (has a character manager script)
            CharacterManager damageTarget = collider.GetComponent<CharacterManager>();

            // if there is a character to damage
            if(damageTarget != null)
            {
                // damage character
                DamageTarget(damageTarget);
            }
        }

        // instantiate explosion particles
        GameObject eParticle = Instantiate(explosionParticle, transform.position, Quaternion.Euler(0,0,0));
        // destroy particles after specified time has passed
        Destroy(eParticle, 2.5f);
        // destroy the dynamite game object
        Destroy(gameObject);
    }
}

