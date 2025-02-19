using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Object")]
    // object to spawn assigned in inspector
    [SerializeField] GameObject objectPrefab;
    // instantiated copy of assigned object
    [SerializeField] GameObject instantiatedGameObject;

    private void Awake()
    {
        //
    }

    private void Start()
    {
        // spawn object specified by WorldObjectManager
        WorldObjectManager.instance.SpawnObject(this);
        // hides the object spawner game object
        gameObject.SetActive(false);
    }

    public void AttemptToSpawnObject()
    {
        if(objectPrefab != null)
        {
            // instantiate the object
            instantiatedGameObject = Instantiate(objectPrefab);
            // sets the transform and rotation of object being spawned to that of the spawner 
            instantiatedGameObject.transform.position = transform.position;
            instantiatedGameObject.transform.rotation = transform.rotation;
        }
    }
}
