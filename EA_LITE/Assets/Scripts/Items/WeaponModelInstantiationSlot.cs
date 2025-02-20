using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModelInstantiationSlot : MonoBehaviour
{
    // WHAT SLOT IS THIS 
    public WeaponModelSlot weaponSlot;
    public GameObject currentWeaponModel;

    public void UnloadWeapon()
    {
        if(currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }
    }

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
