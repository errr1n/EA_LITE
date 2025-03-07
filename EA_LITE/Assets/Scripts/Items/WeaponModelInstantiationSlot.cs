using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModelInstantiationSlot : MonoBehaviour
{
    // WHAT SLOT IS THIS (enum, assigned in inspector, tells us where this slot is, in our case the right hand)
    public WeaponModelSlot weaponSlot;
    //game object to hold the current weapon model
    public GameObject currentWeaponModel;

    // remove weapon
    public void UnloadWeapon()
    {
        if(currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }
    }

    // load weapon
    public void LoadWeapon(GameObject weaponModel)
    {
        // sets the current weapon model to the weapon passed
        currentWeaponModel = weaponModel;
        // parent the weapon model to the transform of object this script is on
        weaponModel.transform.parent = transform;

        // set the weapon model transform properties to the local transform properties from the prefab
        weaponModel.transform.localPosition = Vector3.zero;
        weaponModel.transform.localRotation = Quaternion.identity;
        weaponModel.transform.localScale = Vector3.one;
    }
}
