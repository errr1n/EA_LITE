using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : CharacterInventoryManager
{
    public WeaponItem currentRightHandWeapon;

    // A WAT TO SWAP TO DYNAMITE SHOULD WE CHOOSE?
    [Header("Quick Slots")]
    public WeaponItem[] weaponsInRightHandSlot = new WeaponItem[2]; // I ONLY HAVE A MAXIMUM OF TWO WEAPONS IN THE RIGHT HAND
    public int rightHandWeaponIndex = 0;
}
