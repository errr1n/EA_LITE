using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;

public class AICharacterSpawner : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] GameObject characterGameObject;
    [SerializeField] GameObject instantiatedGameObject;

    private void Awake()
    {
        // WorldAIManager.instance.aiCharacterSpawners.Add(this);
        // WorldAIManager.instance.SpawnCharacter(this);
        // gameObject.SetActive(false);
    }

    private void Start()
    {
        // Debug.Log("Start");
        WorldAIManager.instance.SpawnCharacter(this);
        gameObject.SetActive(false);
    }

    public void AttemptToSpawnCharacter()
    {
        // Debug.Log("AttemptToSpawnCharacter");
        if(characterGameObject != null)
        {
            instantiatedGameObject = Instantiate(characterGameObject);
            // sets the characetrs transform and rotation to that of the spawner 
            instantiatedGameObject.transform.position = transform.position;
            instantiatedGameObject.transform.rotation = transform.rotation;
            // instantiatedGameObject.Get
            WorldAIManager.instance.AddCharacterToSpawnedCharactersList(instantiatedGameObject.GetComponent<AICharacterManager>());
        }
    }
}
