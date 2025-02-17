using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossCharacterManager : AICharacterManager
{
    // GIVE THIS AI A UNIQUE ID
    public int bossID = 0;
    // [SerializeField] in fogWallID = 0;

    [Header("Status")]
    // [SerializeField] bool hasBeenDefeated = false;
    // [SerializeField] bool hasBeenAwakened = false;
    [SerializeField] List<FogWallInteractable> fogWalls;
    [SerializeField] string sleepAnimation;
    [SerializeField] string awakeAnimation;

    [SerializeField] bool _hasBeenDefeated = false;
    public bool HasBeenDefeated{
        get{return _hasBeenDefeated;}
        set{
            _hasBeenDefeated = value;
        }
    }

    [SerializeField] bool _hasBeenAwakened = false;
    public bool HasBeenAwakened{
        get{return _hasBeenAwakened;}
        set{
            _hasBeenAwakened = value;
        }
    }

    [Header("States")]
    [SerializeField] BossSleepState sleepState;

    // IF THE SAVE FILE DOES NOT CONTAIN A BOSS MONSTER OF THIS ID ADD IT
    // IF IT IS PRESENT, CHECK IF BOSS HAS BEEN DEFEATED
    // IF THE BOSS WAS DEFEATED, DIABLE THE GAMEOBJECT
    // IF THE BOSS HAS NOT BEEN DEFEATED, ALLOW THIS OBJECT TO CONTINUE TO BE ACTIVE

    // [Header("DEBUG")]
    // [SerializeField] bool wakeBossUp = false;

    protected override void Awake()
    {
        base.Awake();

        sleepState = Instantiate(sleepState);
        currentState = sleepState;
    }

    protected override void Update()
    {
        base.Update();

        // if(wakeBossUp)
        // {
        //     wakeBossUp = false;
        //     WakeBoss();
        // }
        
        OnSpawn();
    }

    public void OnSpawn()
    {
        // sleepState = Instantiate(sleepState);
        // currentState = sleepState;
        
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

        if(HasBeenAwakened)
        {
            for(int i = 0; i < fogWalls.Count; i++)
            {
                fogWalls[i].IsActive = true;
            }
        }

        if(HasBeenDefeated)
        {
            for(int i = 0; i < fogWalls.Count; i++)
            {
                fogWalls[i].IsActive = false;
            }

            Debug.Log("SET BOSS TO FALSE");
        }

        if(!HasBeenAwakened)
        {
            characterAnimatorManager.PlayTargetActionAnimation(sleepAnimation, true);
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
        // where you would add boss to list
        if(!HasBeenAwakened)
        {
            characterAnimatorManager.PlayTargetActionAnimation(awakeAnimation, true);
        }
        
        HasBeenAwakened = true;
        currentState = idle;
        Debug.Log("idle state");

        // for(int i = 0; i < fogWalls.Count; i++)
        // {
        //     fogWalls[i].IsActive = true;
        // }
    }

}
