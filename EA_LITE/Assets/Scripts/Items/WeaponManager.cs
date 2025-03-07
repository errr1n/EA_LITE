using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // get the damage collider for the weapon
    [SerializeField] public MeleeWeaponDamageCollider meleeDamageCollider;

    private void Awake()
    {
        meleeDamageCollider = GetComponentInChildren<MeleeWeaponDamageCollider>();
    }

    public void SetWeaponDamage(CharacterManager characterWieldingWeapon, WeaponItem weapon)
    {
        // set the character causing damage to the character wielding the weapon
        meleeDamageCollider.characterCausingDamage = characterWieldingWeapon;
        // set the physical damage of the wepaon to the damage collider
        meleeDamageCollider.physicalDamage = weapon.physicalDamage;
    }
}
