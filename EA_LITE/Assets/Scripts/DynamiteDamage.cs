using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteDamage : DamageCollider
{
    [SerializeField] public int dynamiteDamage = 50;

    public CharacterManager setDamageTarget;

    [SerializeField] GameObject explosionParticle;
    GameObject pParticle;

    // public DynamiteWeaponItemAction dynamiteItem;

    // [SerializeField] GameObject poisonParticle;
    // GameObject pParticle;

    // Vector3 bulletPosition;
    // Vector3 dynamitePosition;

  
    
    // Start is called before the first frame update
    void Start()
    {
        // DelayDestroy();
        // StartCoroutine(Delay());

        // dynamiteItem = GetComponent<DynamiteWeaponItemAction>();

        // dynamiteDamage.CheckIfDamageable();
    }

    // Update is called once per frame
    void Update()
    {
        // Invoke(nameof(DestroyShot), 2f);
        StartCoroutine(CheckIfDamageable());
        // CheckIfDamageable();
    }

    // private void OnTriggerEnter(Collider other)
    // {
      
    //     float timer;
    //     if(other.GetComponent<BulletTarget>() != null)
    //     {
    //         //hit target (can play particles from here)
    //         //play particle effect (effet rotated 90 degrees))
    //         pParticle = Instantiate(poisonParticle, transform.position, Quaternion.Euler(90,0,0));
    //         //destory particle after 3 seconds
    //         Destroy(pParticle, 3);
   
    //         // calls check if burnable method
    //         CheckIfBurnable();
    //     } 
    //     //destroy bullet
    //     Destroy(gameObject);
      
    // }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     // float timer;
    //     if(collision.collider.tag == "isGround")
    //     {
    //         //hit target (can play particles from here)
    //         //play particle effect (effet rotated 90 degrees))
    //         // pParticle = Instantiate(poisonParticle, transform.position, Quaternion.Euler(90,0,0));
    //         //destory particle after 3 seconds
    //         // Destroy(pParticle, 3);
   
    //         // calls check if burnable method
    //         CheckIfDamageable();
           
    //         // bulletPosition = transform.position;
    //         // Debug.Log(transform.position);
    //         // Debug.Log("bullet position " + bulletPosition);
    //     } 
    //     //destroy bullet
    //     Destroy(gameObject);

        
    // }

    //check if an AOETarget is within the sphere
    public IEnumerator CheckIfDamageable()
    {
        yield return new WaitForSeconds(3f);
        // setDamageTarget = character;

        // dynamitePosition = dynamiteItem.instantiatedGameObject.transform.position;
        //creates a list of colliders and creates a 4m overlap sphere (sphere collider)
        Collider[] colliders = Physics.OverlapSphere(transform.position, 4f);
        foreach(Collider c in colliders)
        {
            //if their is an AOETarget script attached to object
            if(c.GetComponent<AOETarget>())
            {
                CharacterManager damageTarget = c.GetComponent<CharacterManager>();
                setDamageTarget = damageTarget;

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

                    // c.GetComponent<AOETarget>().ApplyDamage(dynamiteDamage);
                }

                //apply the burn method to the object with the AOETarget script
                // c.GetComponent<AOETarget>().ApplyDamage(dynamiteDamage);

                // character.ProcessCharacterDamage();
                // DamageTarget(setDamageTarget);
                // Debug.Log("0");
            }
        }
        pParticle = Instantiate(explosionParticle, transform.position, Quaternion.Euler(0,0,0));
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

