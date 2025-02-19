using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteDamage : DamageCollider
{
    // public CharacterManager setDamageTarget;
    [SerializeField] public int dynamiteDamage = 50;

    [SerializeField] GameObject explosionParticle;
    GameObject eParticle;

    void Update()
    {
        StartCoroutine(CheckIfDamageable());
    }

    //check if an AOETarget is within the sphere
    public IEnumerator CheckIfDamageable()
    {
        yield return new WaitForSeconds(3f);
        // setDamageTarget = character;

        // dynamitePosition = dynamiteItem.instantiatedGameObject.transform.position;
        //creates a list of colliders and creates a 4m overlap sphere (sphere collider)
        Collider[] colliders = Physics.OverlapSphere(transform.position, 4f);
        foreach(Collider collider in colliders)
        {
            //if their is an AOETarget script attached to object
            if(collider.GetComponent<AOETarget>())
            {
                CharacterManager damageTarget = collider.GetComponent<CharacterManager>();
                // setDamageTarget = damageTarget;

                if(damageTarget != null)
                {
                    // point where colliders touched
                    // contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                    // CHECK IF WE CAN DAMAGE THIS TARGET (BLOCKING)

                    // CHECK IF INVULNERABLE (DODGE)
                    // if(damageTarget.isInvulnerable)
                    // {
                    //     return;
                    // }
                    // StartCoroutine(Delay());
                    //DAMAGE the character
                    DamageTarget(damageTarget);

                    // pParticle = Instantiate(explosionParticle, transform.position, Quaternion.Euler(0,0,0));

                    //PRINT WHAT COLLIDER IS HIT
                    // Debug.Log(damageTarget);

                    // collider.GetComponent<AOETarget>().ApplyDamage(dynamiteDamage);
                }

                //apply the burn method to the object with the AOETarget script
                // collider.GetComponent<AOETarget>().ApplyDamage(dynamiteDamage);

                // character.ProcessCharacterDamage();
                // DamageTarget(setDamageTarget);
                // Debug.Log("0");
            }
        }
        eParticle = Instantiate(explosionParticle, transform.position, Quaternion.Euler(0,0,0));
        Destroy(eParticle, 2.5f);
        Destroy(gameObject);
    }

    // private IEnumerator Delay()
    // {
    //     // Debug.Log("Delay");
    //     // CheckIfDamageable(setDamageTarget);
    //     yield return new WaitForSeconds(3f);
    //     DamageTarget(setDamageTarget);
    //     Debug.Log(setDamageTarget);
    // }

    // private void DelayDestroy()
    // {
    //     // Destroy(gameObject, 3f);
    // }
}

