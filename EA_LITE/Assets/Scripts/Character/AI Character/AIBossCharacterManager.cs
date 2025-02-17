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
    
    private bool AttemptToSpawnHPBar = true;

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

    [SerializeField] bool _bossFightIsActive = false;
    public bool BossFightIsActive{
        get{return _bossFightIsActive;}
        set{
            OnBossFightIsActiveChanged(_bossFightIsActive, value);
            _bossFightIsActive = value;
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

        // PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue(characterStatsManager.CurrentHealth);
        // ActivateHPBar();
        CheckIfHPBarIsActive();
    }

    public void OnSpawn()
    {
        // BossFightIsActive = 

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

    public override IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
    {
        characterStatsManager.CurrentHealth = 0;
        isDead = true;

        BossFightIsActive = false;

        // RESET ANY FLAGS THAT NEED TO BE RESET

        // IF WE ARE NOT GROUNDED, PLAY AERIAL DEATH ANIMATION

        if(!manuallySelectDeathAnimation)
        {
            // ANIMATION
            characterAnimatorManager.PlayTargetActionAnimation("Death", true);
        }

        // PLAY SOME DEATH SFX

        yield return new WaitForSeconds(5);

        // DISABLE CHARACTER
    }

    public void WakeBoss()
    {
        // where you would add boss to list
        if(!HasBeenAwakened)
        {
            characterAnimatorManager.PlayTargetActionAnimation(awakeAnimation, true);
        }
        
        BossFightIsActive = true;
        HasBeenAwakened = true;
        currentState = idle;
        Debug.Log("Idle");
    }

    private void OnBossFightIsActiveChanged(bool oldStatus, bool newStatus)
    {
        if(BossFightIsActive)
        {
            // AttemptToSpawnHPBar = false;
            // Debug.Log("Here");

            // CREATE A HEALTH BAR FOR THE BOSS THAT IS IN THE FIGHT (IF ACTIVE)
            GameObject bossHealthBar = Instantiate(PlayerUIManager.instance.playerUIHudManager.bossHealthBarObject, PlayerUIManager.instance.playerUIHudManager.bossHealthBarParent);

            UI_Boss_HP_Bar bossHPBar = bossHealthBar.GetComponentInChildren<UI_Boss_HP_Bar>();

            // this meaning this AI Boss Character Manager
            bossHPBar.EnableBossHPBar(this);


            // DESTROY ANY HP BARS CURRENTLY ACTIVE (IF THE BOSS IS NO LONGER ACTIVE)
        }
    }

    private void CheckIfHPBarIsActive()
    {
        if(BossFightIsActive)
        {
            if(AttemptToSpawnHPBar)
            {
                AttemptToSpawnHPBar = false;
                BossFightIsActive = true;
            }
        }
    }

    // private void ActivateHPBar()
    // {
    //     if(AttemptToSpawnHPBar)
    //     {
    //         // if(BossFightIsActive)
    //         // {
    //         //     AttemptToSpawnHPBar = false;

    //         //     // CREATE A HEALTH BAR FOR THE BOSS THAT IS IN THE FIGHT (IF ACTIVE)
    //         //     GameObject bossHealthBar = Instantiate(PlayerUIManager.instance.playerUIHudManager.bossHealthBarObject, PlayerUIManager.instance.playerUIHudManager.bossHealthBarParent);

    //         //     UI_Boss_HP_Bar bossHPBar = bossHealthBar.GetComponentInChildren<UI_Boss_HP_Bar>();

    //         //     // this meaning this AI Boss Character Manager
    //         //     bossHPBar.EnableBossHPBar(this);


    //         //     // DESTROY ANY HP BARS CURRENTLY ACTIVE (IF THE BOSS IS NO LONGER ACTIVE)
    //         // }
    //     }
    // }

}
