using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    public WallHealth wallHealth;
    public GameObject wall;

    public bool hasHealth = true;

    void Start()
    {
        hasHealth = true;
    }

    void Update()
    {
        CheckWallHealth();
    }

    public void CheckWallHealth()
    {
        if(wallHealth.curHealth <= 0)
        {
            hasHealth = false;
            Destroy(wall);
        }
    }

    public void ProcessDamage(DamageableObject damagedObject, float physicalDamage)
    {
        // call the call damage effect
        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);

        // get physical damage amount
        damageEffect.physicalDamage = physicalDamage;

        Debug.Log("damage wall for: " + physicalDamage);
        wallHealth.ReceiveDamage(physicalDamage);
    }

}
