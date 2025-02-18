using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "A.I/States/Spit Attack")]

public class SpitAttackState : AIState
{
    // [Header("Current Attack")]
    // [HideInInspector] public AICharacterAttackAction currentAttack;
    // [HideInInspector] public bool willPerformCombo = false;

    // [Header("State Flags")]
    // [SerializeField] protected bool hasPerformedAttack = false;
    // protected bool hasPerformedCombo = false;

    // [Header("Pivot After Attack")]
    // [SerializeField] protected bool pivotAfterAttack = false;

    // public override AIState Tick(AICharacterManager aiCharacter)
    // {
    //     // if target is null, return to idle
    //     if(aiCharacter.aiCharacterCombatManager.currentTarget == null)
    //     {
    //         return SwitchState(aiCharacter, aiCharacter.idle);
    //     }

    //     // if target is dead, return to idle
    //     if(aiCharacter.aiCharacterCombatManager.currentTarget.isDead)
    //     {
    //         return SwitchState(aiCharacter, aiCharacter.idle);
    //     }

    //     //rotate towards the target while attacking
    //     aiCharacter.aiCharacterCombatManager.RotateTowardsTargetWhileAttacking(aiCharacter);

    //     //set movement to 0
    //     aiCharacter.characterAnimatorManager.UpdateAnimatorMovementParameters(0, 0);

    //     // perform a combo
    //     if(willPerformCombo && !hasPerformedCombo)
    //     {
    //         // if(currentAttack.comboAction != null)
    //         // {
    //         //     // if can combo
    //         //     // hasPerformedAttack = true;
    //         //     // currentAttack.comboAction.AttemptToPerformAction(aiCharacter);
    //         // }
    //     }

    //     if(aiCharacter.isPerformingAction)
    //     {
    //         return this;
    //     }

    //     if(!hasPerformedAttack)
    //     {
    //         // if we are still recovering from an action, wait before performing another 
    //         if(aiCharacter.aiCharacterCombatManager.actionRecoveryTimer > 0)
    //         {
    //             return this;
    //         }

    //         PerformAttack(aiCharacter);

    //         // return to the top, so if we have a combo we process that when we are able
    //         return this;
    //     }

    //     if(pivotAfterAttack)
    //     {
    //         aiCharacter.aiCharacterCombatManager.PivotTowardsTarget(aiCharacter);
    //     }

    //     return SwitchState(aiCharacter, aiCharacter.pursueTarget);
    // }

    // protected void PerformAttack(AICharacterManager aiCharacter)
    // {
    //     hasPerformedAttack = true;
    //     currentAttack.AttemptToPerformAction(aiCharacter);
    //     aiCharacter.aiCharacterCombatManager.actionRecoveryTimer = currentAttack.actionRecoveryTime;
    // }

    // protected override void ResetStateFlags(AICharacterManager aiCharacter)
    // {
    //     base.ResetStateFlags(aiCharacter);

    //     hasPerformedAttack = false;
    //     hasPerformedCombo = false;
    // }

    [Header("Attacks")]
    public List<AICharacterAttackAction> aiCharacterAttacks;   // a list of all possible attack actions for this character
    public List<AICharacterAttackAction> potentialAttacks;  // a list that is created during this state. all attacks possible in this situation (based on angle, distance etc.)
    public AICharacterAttackAction chosenAttack;
    public AICharacterAttackAction previousAttack;
    protected bool hasAttack = false;

    [Header("Combo")]
    [SerializeField] protected bool canPerformCombo = false;   // can character can perform combo attack, after the initial attack
    [SerializeField] protected int chanceToPerformCombo = 25;  // the chance (%) of the character to perform a combo on the next attack
    protected bool hasRolledForComboChance = false;                 // if we have already rolled for the chance duriong this state

    // [SerializeField] private int chanceToPerformSpitAtttack = 50;

    // [Header("Pivot")]
    // [SerializeField] protected bool enablePivot;

    [Header("Engagement Distance")]
    // WAS 5
    [SerializeField] public float maximumEngagementDistance = 3.1f; // the distance we have to be away from the target before we enter the pursue target state

    public override AIState Tick(AICharacterManager aiCharacter)
    {
        // Debug.Log("0");
        if(aiCharacter.isPerformingAction)
        {
            return this;
        }

        // Debug.Log("1");

        if(!aiCharacter.navMeshAgent.enabled)
        {
            aiCharacter.navMeshAgent.enabled = true;
        }

        // Debug.Log("2");

        // if we want the ai character to face and turn towards its target when its outside its fov include this
        if(aiCharacter.aiCharacterCombatManager.enablePivot)
        {
            if(!aiCharacter.IsMoving)
            {
                // Debug.Log("viewableAngle: "+ aiCharacter.aiCharacterCombatManager.viewableAngle);
                if(aiCharacter.aiCharacterCombatManager.viewableAngle < -30 || aiCharacter.aiCharacterCombatManager.viewableAngle > 30)
                {
                    aiCharacter.aiCharacterCombatManager.PivotTowardsTarget(aiCharacter);
                    // Debug.Log("3");
                }
            }
        }

        // rotate to face our target
        aiCharacter.aiCharacterCombatManager.RotateTowardsAgent(aiCharacter);
        // Debug.Log("4");

        // if our target is no longer present, switch back to idle state
        if(aiCharacter.aiCharacterCombatManager.currentTarget == null)
        {
            // Debug.Log("5");
            return SwitchState(aiCharacter, aiCharacter.idle);
        }

        // if we do not have an attack, get one
        if(!hasAttack)
        {
            GetNewAttack(aiCharacter);
            // Debug.Log("6");
        }
        else
        {
            // check recovery timer
            // pass attack to attack state
            aiCharacter.attack.currentAttack = chosenAttack;
            // Debug.Log("7");
            // roll for a combo chance
            return SwitchState(aiCharacter, aiCharacter.attack);
            // switch state
        }

        // if we are outside the combat engagement distance, switch to pursue target state
        if(aiCharacter.aiCharacterCombatManager.distanceFromTarget > maximumEngagementDistance)
        {
            // Debug.Log("roll for 50/50");
            // if(RollForOutcomeChance(chanceToPerformSpitAtttack))
            // {
            //     return SwitchState(aiCharacter, aiCharacter.spitAttack);
            // }

            return SwitchState(aiCharacter, aiCharacter.pursueTarget);
        }
        // Debug.Log("8");

        NavMeshPath path = new NavMeshPath();
        aiCharacter.navMeshAgent.CalculatePath(aiCharacter.aiCharacterCombatManager.currentTarget.transform.position, path);
        aiCharacter.navMeshAgent.SetPath(path);
        // Debug.Log("9");

        return this;
    }

    protected virtual void GetNewAttack(AICharacterManager aiCharacter)
    {
        // 1. sort through all possible attacks
        potentialAttacks = new List<AICharacterAttackAction>();
        // Debug.Log("0");

        // 2. remove attacks that can't be used in this situation (based on angle and distance)
        foreach(var potentialAttack in aiCharacterAttacks)
        {
            // Debug.Log("1");
            // check if we are too close to perform attack
            if(potentialAttack.minimumAttackDistance > aiCharacter.aiCharacterCombatManager.distanceFromTarget)
            {
                // Debug.Log("2");
                continue;
            }

            // check if we are too far to perform attack
            if(potentialAttack.maximumAttackDistance < aiCharacter.aiCharacterCombatManager.distanceFromTarget)
            {
                // Debug.Log("3");
                continue;
            }

            // check if the target is outside of the minimum field of view
            if(potentialAttack.minimumAttackAngle > aiCharacter.aiCharacterCombatManager.viewableAngle)
            {
                // Debug.Log("4");
                continue;
            }
            
            // check if the target is outside of the maximum field of view
            if(potentialAttack.maximumAttackAngle < aiCharacter.aiCharacterCombatManager.viewableAngle)
            {
                // Debug.Log("5");
                continue;
            }

            // Debug.Log("6");
            // 3. place remaining attacks into a list
            potentialAttacks.Add(potentialAttack);
        }

        // 4. pick an attack from remaining list randomly, based on weight
        if(potentialAttacks.Count <= 0)
        {
            Debug.Log("NO ATTACKS");
            return;
        }

        var totalWeight = 0;

        foreach(var attack in potentialAttacks)
        {
            totalWeight += attack.attackWeight;
        }

        var randomWeightValue = Random.Range(1, totalWeight + 1);
        var processedWeight = 0;

        foreach(var attack in potentialAttacks)
        {
            processedWeight += attack.attackWeight;
            
            // 5. select this attack and pass it to the attack state
            if(randomWeightValue <= processedWeight)
            {
                // this is the attack
                chosenAttack = attack;
                previousAttack = chosenAttack;
                hasAttack = true;
                return;
            }
        }
    }

    protected virtual bool RollForOutcomeChance(int outcomeChance)
    {
        bool outcomeWillBePerformed = false;

        int randomPercentage = Random.Range(0, 101);

        if(randomPercentage < outcomeChance)
        {
            outcomeWillBePerformed = true;
        }

        Debug.Log("outcomeWillBePerformed: " + outcomeWillBePerformed);
        return outcomeWillBePerformed;
    }

    protected override void ResetStateFlags(AICharacterManager aiCharacter)
    {
        base.ResetStateFlags(aiCharacter);

        hasAttack = false;
        hasRolledForComboChance = false;
    }
}
