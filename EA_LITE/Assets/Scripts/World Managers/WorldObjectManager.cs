using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjectManager : MonoBehaviour
{
    // 1. create an object script that will hold the logic for the walls
    // 2. create general object spawner script and prefab
    // 3. when the walls are spawned, add them to the world wall list
    // 4. grab the correct wall from the list on the boss manager when the boss is being initialized

    public static WorldObjectManager instance;

    [Header("Objects")]
    // list of active objectSpawners in game
    [SerializeField] List<ObjectSpawner> objectSpawners;
    // list of active objects spawned in game
    [SerializeField] List<GameObject> spawnedInObjects;

    [Header("Walls")]
    // list of wall objects in game
    public List<WallInteractable> walls;

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

    public void SpawnObject(ObjectSpawner objectSpawner)
    {
        // adds object spawner to the list objectSpawners
        objectSpawners.Add(objectSpawner);
        // attempt to spawn object from specified object spawner
        objectSpawner.AttemptToSpawnObject();
    }

    public void AddWallToList(WallInteractable wall)
    {
        if(!walls.Contains(wall))
        {
            walls.Add(wall);
        }
    }

    public void RemoveWallFromList(WallInteractable wall)
    {
        if(walls.Contains(wall))
        {
            walls.Remove(wall);
        }
    }
}
