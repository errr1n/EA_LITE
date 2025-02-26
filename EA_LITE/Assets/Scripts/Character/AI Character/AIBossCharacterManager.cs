using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossCharacterManager : AICharacterManager
{
    // access boss HP bar
    public UI_Boss_HP_Bar bossHPBar;
    // GIVE THIS AI A UNIQUE ID
    public int bossID = 0;
    // check if boss HP bar still needs to be spawned
    private bool attemptToSpawnHPBar = true;
    //list of walls
    [SerializeField] List<WallInteractable> walls;
    // public SpitAttackState spitAttack;

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
            _bossFightIsActive = value;
        }
    }

    [Header("Status")]
    // animation strings
    [SerializeField] string sleepAnimation;
    [SerializeField] string awakeAnimation;

    [Header("States")]
    [SerializeField] BossSleepState sleepState;

    // IF THE SAVE FILE DOES NOT CONTAIN A BOSS MONSTER OF THIS ID ADD IT
    // IF IT IS PRESENT, CHECK IF BOSS HAS BEEN DEFEATED
    // IF THE BOSS WAS DEFEATED, DISABLE THE GAMEOBJECT
    // IF THE BOSS HAS NOT BEEN DEFEATED, ALLOW THIS OBJECT TO CONTINUE TO BE ACTIVE

    protected override void Awake()
    {
        base.Awake();

        // set boss state to sleep 
        sleepState = Instantiate(sleepState);
        currentState = sleepState;
    }

    protected override void Update()
    {
        base.Update();

        OnSpawn();

        // check if boss fight is active
        IsBossFightActive(BossFightIsActive);
    }

    public void OnSpawn()
    {
        // BossFightIsActive = 

        // create a new list to hold walls matching bossIDs
        walls = new List<WallInteractable>();

        foreach(var wall in WorldObjectManager.instance.walls)
        {
            // if the wallID matches the bossID
            if(wall.wallID == bossID)
            {
                // add the wall to the list
                walls.Add(wall);
            }
        }

        // if boss has been awakened
        if(HasBeenAwakened)
        {
            // check the new walls list and get the matching wall
            for(int i = 0; i < walls.Count; i++)
            {
                // set the wall to active
                walls[i].IsActive = true;
            }
        }

        // if boss has been defeated
        if(HasBeenDefeated)
        {
            // check the new walls list and get the matching wall
            for(int i = 0; i < walls.Count; i++)
            {
                // despawn the wall
                walls[i].IsActive = false;
            }
        }

        // if boss has not been awakened
        if(!HasBeenAwakened)
        {
            // play sleep animation
            characterAnimatorManager.PlayTargetActionAnimation(sleepAnimation, true);
        }
    }

    public override IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
    {
        PlayerUIManager.instance.playerUIPopUpManager.SendBossDefeatedPopUp("ROCK TITAN DEFEATED");
        // set boss health to 0
        characterStatsManager.CurrentHealth = 0;
        // update the health bar to show 0
        bossHPBar.SetBossHP(characterStatsManager.CurrentHealth);
        
        // change isDead flag to true
        isDead = true;

        // change boos fight is active flag to false
        BossFightIsActive = false;

        // RESET ANY FLAGS THAT NEED TO BE RESET
        // HasBeenDefeated = true;

        // IF WE ARE NOT GROUNDED, PLAY AERIAL DEATH ANIMATION

        if(!manuallySelectDeathAnimation)
        {
            // ANIMATION
            characterAnimatorManager.PlayTargetActionAnimation("Death", true);
        }

        // PLAY SOME DEATH SFX

        yield return new WaitForSeconds(5);

        // DISABLE CHARACTER

        yield return new WaitForSeconds(3);
        //return to main menu
        StartCoroutine(WorldSaveGameManager.instance.MainMenu());
    }

    public void WakeBoss()
    {
        // where you would add boss to list

        // if boss has not been awakened
        if(!HasBeenAwakened)
        {
            // play an awake aniamtion
            characterAnimatorManager.PlayTargetActionAnimation(awakeAnimation, true);
        }
        
        // set boss flags to true
        BossFightIsActive = true;
        HasBeenAwakened = true;
        // change the bosses state to idle
        currentState = idle;
    }

    private void IsBossFightActive(bool newStatus)
    {
        //if the boss fight is active
        if(BossFightIsActive)
        {
            // CREATE A HEALTH BAR FOR THE BOSS THAT IS IN THE FIGHT (IF ACTIVE)
            GameObject bossHealthBar;

            // DESTROY ANY HP BARS CURRENTLY ACTIVE (IF THE BOSS IS NO LONGER ACTIVE)
            if(attemptToSpawnHPBar)
            {
                // only spawn one HP bar
                attemptToSpawnHPBar = false;

                // intantiate the HP bar object at the health bar parent transform
                bossHealthBar = Instantiate(PlayerUIManager.instance.playerUIHudManager.bossHealthBarObject, PlayerUIManager.instance.playerUIHudManager.bossHealthBarParent);

                // get the health bar slider from the parent boss health bar object
                bossHPBar = bossHealthBar.GetComponentInChildren<UI_Boss_HP_Bar>();

                // enable this health bar, this meaning this AI Boss Character Manager
                bossHPBar.EnableBossHPBar(this);
            }

            // set the health bar value to the bosses current health value
            bossHPBar.SetBossHP(characterStatsManager.CurrentHealth);
        }
    }
}
