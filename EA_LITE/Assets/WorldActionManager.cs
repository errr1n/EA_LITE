using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorldActionManager : MonoBehaviour
{
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

    // MIGHT NOT BE NECESSARY (24)
    public WeaponItemAction GetWeaponItemActionByID(int ID)
    {
        // SEARCHES THE ENTIRE ARRAY FOR ACTION THAT MATCHES ID
        return weaponItemActions.FirstOrDefault(action => action.actionID == ID);
    }
}
