using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorldActionManager : MonoBehaviour
{
    // ------------ NOT CURRENTLY IN USE -------------

    public static WorldActionManager instance;

    [Header("Weapon Item Actions")]
    public WeaponItemAction[] weaponItemActions;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for(int i = 0; i < weaponItemActions.Length; i++)
        {
            weaponItemActions[i].actionID = i;
        }
    }

    public WeaponItemAction GetWeaponItemActionByID(int ID)
    {
        // searches the entire array for action that matches id
        return weaponItemActions.FirstOrDefault(action => action.actionID == ID);
    }
}
