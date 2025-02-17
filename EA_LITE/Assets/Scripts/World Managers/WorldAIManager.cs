using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorldAIManager : MonoBehaviour
{
    public static WorldAIManager instance;

    // [Header("DEBUG")]
    // [SerializeField] bool despawnCharacters = false;
    // [SerializeField] bool respawnCharacters = false;

    [Header("Characters")]
    [SerializeField] List<AICharacterSpawner> aiCharacterSpawners;
    // [SerializeField] public GameObject[] aiCharacters;
    [SerializeField] List<AICharacterManager> spawnedInCharacters;

    [Header("Bosses")]
    [SerializeField] List<AIBossCharacterManager> spawnedInBosses;
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

    public void SpawnCharacter(AICharacterSpawner aiCharacterSpawner)
    {
        aiCharacterSpawners.Add(aiCharacterSpawner);
        aiCharacterSpawner.AttemptToSpawnCharacter();
    }

    public void AddCharacterToSpawnedCharactersList(AICharacterManager character)
    {
        if(spawnedInCharacters.Contains(character))
        {
            return;
        }

        spawnedInCharacters.Add(character);

        AIBossCharacterManager bossCharacter = character as AIBossCharacterManager;

        if(bossCharacter != null)
        {
            if(spawnedInBosses.Contains(bossCharacter))
            {
                return;
            }

            spawnedInBosses.Add(bossCharacter);
        }
    }

    public AIBossCharacterManager GetBossCharacterByID(int ID)
    {
        // check list of spawned in bosses, look for first one that matches ID, if exists, check that it matches given ID
        return spawnedInBosses.FirstOrDefault(boss => boss.bossID == ID);
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

    private void DisableAllCharacters()
    {
        //
    }

}
