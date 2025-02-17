using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjectManager : MonoBehaviour
{
    // 1. create an object script that will hold the logic for the fog walls
    // 2. create general object spawner script and prefab
    // 3. when the fog walls are spawned, add them to the world fog wall list
    // 4. grab the correct fogWall from the list on the boss manager when the boss is being initialized

    public static WorldObjectManager instance;

    [Header("Objects")]
    [SerializeField] List<ObjectSpawner> objectSpawners;
    // [SerializeField] public GameObject[] aiCharacters;
    [SerializeField] List<GameObject> spawnedInObjects;
    // [SerializeField] GameObject instantiatedCharacter;

    [Header("Fog Walls")]
    // public List<FogWallInteractable> fogWalls;
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
        objectSpawners.Add(objectSpawner);
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
