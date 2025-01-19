using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorldItemDatabase : MonoBehaviour
{
    public static WorldItemDatabase instance;

    //unarmed?

    [Header("Weapons")]
    [SerializeField] List<WeaponItem> weapons = new List<WeaponItem>();

    [Header("Items")]
    //A list of every item in the game
    private List<Item> items = new List<Item>();


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

        // ADD ALL OR OUR WEAPONS TO OUR LIST OF ITEMS
        foreach(var weapon in weapons)
        {
            items.Add(weapon);
        }

        // ASSIGN ALL OUR ITEMS A UNIQUE ITEM ID
        for (int i = 0; i < items.Count; i++)
        {
            items[i].itemID = i;
        }
    }

    // CHECK THE LIST FOR AN ID THAT MATCHES
    public WeaponItem GetWeaponByID(int ID)
    {
        // PASS BACK WEAPON THE MATCHES
        return weapons.FirstOrDefault(weapon => weapon.itemID == ID);
    }
}
