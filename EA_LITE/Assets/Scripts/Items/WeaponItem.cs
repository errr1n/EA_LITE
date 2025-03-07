using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
    [Header("Weapon Model")]
    public GameObject weaponModel;

    [Header("Weapon Base Damage")]
    public int physicalDamage = 0;

    [Header("Actions")]
    public WeaponItemAction leftClick_Action;

    [Header("Sounds")]
    public AudioClip[] whooshes;
}
