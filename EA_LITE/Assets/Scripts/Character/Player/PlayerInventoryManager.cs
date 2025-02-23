using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : CharacterInventoryManager
{
    // tracks current right hand weapon
    public WeaponItem currentRightHandWeapon;

    [Header("Quick Slots")]
    public int rightHandWeaponIndex = 0;
    public WeaponItem[] weaponsInRightHandSlot = new WeaponItem[2]; // ONLY A MAXIMUM OF TWO WEAPONS IN THE RIGHT HAND
}
