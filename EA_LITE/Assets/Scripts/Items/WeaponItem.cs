using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;

public class WeaponItem : Item
{
    // ANIMATOR CONTROLLER OVERRIDE (change attack animations based on weapon you are using)

    // [SerializeField] public UI_Image itemUIObject;
    // [SerializeField] UI_Image CurrentWeaponImage;

    [Header("Weapon Model")]
    public GameObject weaponModel;

    [Header("Weapon Base Damage")]
    public int physicalDamage = 0;

    // HEAVY VS LIGHT ATTACKS?
    // [Header("Attack Modifiers")]
    // public float light_Attack_Modifier = 1.1f;

    [Header("Actions")]
    public WeaponItemAction leftClick_Action;

    // [Header("Stamina Costs")]
    // public int baseStaminaCost = 20;

    [Header("Sounds")]
    public AudioClip[] whooshes;
}
