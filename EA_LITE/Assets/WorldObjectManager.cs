using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjectManager : MonoBehaviour
{
    static WorldObjectManager instance;

    //1. create an object script that will hold the logic for the fog walls
    // 2. create general object spawner script and prefab
    // 3. when the fog walls are spawned, add them to the world fog wall list
    // 4. grab the correct fogwall from the list on the boss manager when the boss is being initialized

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
}
