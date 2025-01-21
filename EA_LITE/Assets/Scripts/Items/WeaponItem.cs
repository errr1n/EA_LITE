using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    // ANIMATOR CONTROLLER OVERRIDE (change attack animations based on weapon you are using)

    [Header("Weapon Model")]
    public GameObject weaponModel;

    // [Header("Weapon Model")]

    [Header("Weapon Base Damage")]
    public int physicalDamage = 0;

    // HEAVY VS LIGHT ATTACKS?
    // [Header("Attack Modifiers")]
    // public float light_Attack_Modifier = 1.1f;

    [Header("Actions")]
    public WeaponItemAction leftClick_Action;

    // [Header("Stamina Costs")]
    // public int baseStaminaCost = 20;
}
