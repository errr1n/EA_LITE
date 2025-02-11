using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossCharacterManager : AICharacterManager
{
    // GIVE THIS AI A UNIQUE ID
    public int bossID = 0;
    // [SerializeField] in fogWallID = 0;
    [SerializeField] bool hasBeenDefeated = false;
    [SerializeField] bool hasBeenAwakened = false;
    [SerializeField] List<FogWallInteractable> fogWalls;

    // IF THE SAVE FILE DOES NOT CONTAIN A BOSS MONSTER OF THIS ID ADD IT
    // IF IT IS PRESENT, CHECK IF BOSS HAS BEEN DEFEATED
    // IF THE BOSS WAS DEFEATED, DIABLE THE GAMEOBJECT
    // IF THE BOSS HAS NOT BEEN DEFEATED, ALLOW THIS OBJECT TO CONTINUE TO BE ACTIVE

    [Header("DEBUG")]
    [SerializeField] bool wakeBossUp = false;

    protected override void Update()
    {
        base.Update();

        if(wakeBossUp)
        {
            wakeBossUp = false;
            WakeBoss();
        }
        
        OnSpawn();
    }

    public void OnSpawn()
    {
        //LOCATE FOG WALL
        // StartCoroutine(GetFogWallsFromWorldObjectManager());
        fogWalls = new List<FogWallInteractable>();

        foreach(var fogWall in WorldObjectManager.instance.fogWalls)
        {
            if(fogWall.fogWallID == bossID)
            {
                fogWalls.Add(fogWall);
            }
        }

        if(hasBeenAwakened)
        {
            for(int i = 0; i < fogWalls.Count; i++)
            {
                fogWalls[i].IsActive = true;
            }
        }

        if(hasBeenDefeated)
        {
            for(int i = 0; i < fogWalls.Count; i++)
            {
                fogWalls[i].IsActive = false;
            }

            Debug.Log("SET BOSS TO FALSE");
        }
    }

    // private IEnumerator GetFogWallsFromWorldObjectManager()
    // {
    //     while(WorldObjectManager.instance.fogWalls.Count == 0)
    //     {
    //         yield return new WaitForEndOfFrame();
    //     }

    //     fogWalls = new List<FogWallInteractable>();

    //     foreach(var fogWall in WorldObjectManager.instance.fogWalls)
    //     {
    //         if(fogWall.fogWallID == bossID)
    //         {
    //             fogWalls.Add(fogWall);
    //         }
    //     }
    // }

    public void WakeBoss()
    {
        hasBeenAwakened = true;
        // where you would add boss to list
        Debug.Log("wake boss True");

        // for(int i = 0; i < fogWalls.Count; i++)
        // {
        //     fogWalls[i].IsActive = true;
        // }
    }

}
