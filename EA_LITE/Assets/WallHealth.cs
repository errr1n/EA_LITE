using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : MonoBehaviour
{
    [Header("Wall Health")]
    public float maxHealth;
    public float curHealth;


    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;

        // ReceiveDamage(50);
    }

    public void ReceiveDamage(float damageAmount)
    {
        curHealth -= damageAmount;
    }
}
