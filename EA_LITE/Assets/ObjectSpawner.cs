using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] GameObject nGameObject;
    [SerializeField] GameObject instantiatedGameObject;

    private void Awake()
    {
        //
    }

    private void Start()
    {
        WorldObjectManager.instance.SpawnObject(this);
        gameObject.SetActive(false);
    }

    public void AttemptToSpawnObject()
    {
        if(nGameObject != null)
        {
            instantiatedGameObject = Instantiate(nGameObject);
            // sets the characetrs transform and rotation to that of the spawner 
            instantiatedGameObject.transform.position = transform.position;
            instantiatedGameObject.transform.rotation = transform.rotation;
            // instantiatedGameObject.Get
        }
    }
}
