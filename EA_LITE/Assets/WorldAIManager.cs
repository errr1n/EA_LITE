using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldAIManager : MonoBehaviour
{
    public static WorldAIManager instance;

    [Header("DEBUG")]
    [SerializeField] bool despawnCharacters = false;
    [SerializeField] bool respawnCharacters = false;

    [Header("Characters")]
    [SerializeField] public GameObject[] aiCharacters;
    [SerializeField] public List<GameObject> spawnedInCharacters;
    // [SerializeField] GameObject instantiatedCharacter;

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
        StartCoroutine(WaitForSceneToLoadThenSpawnCharacters());
    }

    private void Update()
    {
        if(respawnCharacters)
        {
            respawnCharacters = false;
            SpawnAllCharacters();
        }
        if(despawnCharacters)
        {
            despawnCharacters = false;
            DespawnAllCharacters();
        }
    }

    private IEnumerator WaitForSceneToLoadThenSpawnCharacters()
    {
        while(!SceneManager.GetActiveScene().isLoaded)
        {
            yield return null;
        }

        SpawnAllCharacters();
    }

    private void SpawnAllCharacters()
    {
        foreach(var character in aiCharacters)
        {
            GameObject instantiatedCharacter = Instantiate(character);
            spawnedInCharacters.Add(instantiatedCharacter);
            Debug.Log("SPAWN");
        }
    }

    private void DespawnAllCharacters()
    {

        for (var i = 0; i < spawnedInCharacters.Count; i++)
        {
                // Destroy(spawnedInCharacters[i].aiCharacters);  // Delete the Gameobject
                // spawnedInCharacters.Remove(spawnedInCharacters[i]);      // Delete the List item 

                // spawnedInCharacters.Remove(i);
                // if(spawnedInCharacters.Count == 1)
                // {
                //     GameObject.Destroy(spawnedInCharacters[0]); 
                //     spawnedInCharacters.RemoveAt(0);
                // }

                while(i >= 0)
                {
                    GameObject.Destroy(spawnedInCharacters[i]); 
                    spawnedInCharacters.RemoveAt(i);
                    Debug.Log(i);

                    // if(spawnedInCharacters.Count == 1)
                    // {
                    //     GameObject.Destroy(spawnedInCharacters[0]); 
                    //     spawnedInCharacters.RemoveAt(0);
                    // }
                }
                // if(spawnedInCharacters.Count == 1)
                // {
                //     GameObject.Destroy(spawnedInCharacters[0]); 
                //     spawnedInCharacters.RemoveAt(0);
                // }
                // GameObject.Destroy(spawnedInCharacters[0]); 
                // spawnedInCharacters.RemoveAt(0);
        }

        // foreach(var character in aiCharacters)
        // {
        //     GameObject instantiatedCharacter = aiCharacters[aiCharacters.Length];
        // //     spawnedInCharacters.Remove(instantiatedCharacter);
        // //     Destroy(instantiatedCharacter);
        // //     Debug.Log("REMOVE");
        // }

        // foreach(var character in aiCharacters)
        // {
        //     GameObject instantiatedCharacter = aiCharacters[aiCharacters.Length];
        //     spawnedInCharacters.Remove(instantiatedCharacter);
        //     Debug.Log("DESPAWN");
        // }
        // for (var i = 0; i < spawnedInCharacters.Count; i++)
        // {
        //     // gonna need a despawn
        //     spawnedInCharacters.RemoveAt(i);
        //     Debug.Log("DESPAWN");
        // }

        // foreach(var character in aiCharacters)
        // {
        //     // GameObject instantiatedCharacter = Instantiate(character);
        //     spawnedInCharacters.Remove(instantiatedCharacter);
        //     Debug.Log("SPAWN");
        // }

        // foreach(var character in aiCharacters)
        // {
        //     // GameObject instantiatedCharacter = Instantiate(character);
        //     Destroy(character);
        //     Debug.Log("DESPAWN");
        // }

        // for (var i = 0; i < spawnedInCharacters.Count; i++)
        // {
	    //     // WP.RemoveAt(i);
        //     // Destroy(instantiatedCharacter);
        //     GameObject instantiatedCharacter = aiCharacters[aiCharacters.Length];
        //     spawnedInCharacters.Remove(instantiatedCharacter);
        //     Destroy(instantiatedCharacter);
        //     Debug.Log("REMOVE");
        // }
    }

    // private DisableAllCharacters()
    // {

    // }
}
