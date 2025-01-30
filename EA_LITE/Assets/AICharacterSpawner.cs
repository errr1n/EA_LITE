using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterSpawner : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] GameObject characterGameObject;
    [SerializeField] GameObject instantiatedGameObject;

    private void Awake()
    {
        // WorldAIManager.instance.aiCharacterSpawners.Add(this);
    }

    private void Start()
    {
        WorldAIManager.instance.SpawnCharacter(this);
        gameObject.SetActive(false);
    }

    public void AttemptToSpawnCharacter()
    {
        if(characterGameObject != null)
        {
            instantiatedGameObject = Instantiate(characterGameObject);
            // sets the characetrs transform and rotation to that of the spawner 
            instantiatedGameObject.transform.position = transform.position;
            instantiatedGameObject.transform.rotation = transform.rotation;
            // instantiatedGameObject.Get
        }
    }
}
