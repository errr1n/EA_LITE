using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallHealth : MonoBehaviour
{
    [Header("Wall Health")]
    public float maxHealth;
    public float curHealth;

    public Slider healthBar;


    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = curHealth;
    }

    public void ReceiveDamage(float damageAmount)
    {
        curHealth -= damageAmount;
        healthBar.value = curHealth;
    }
}
