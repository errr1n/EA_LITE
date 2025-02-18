using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "A.I/States/Combat Stance")]

public class CombatStanceState : AIState
{
    // 1. select an attack from the attack states, depending on distance and angle of target in relation to character
    // 2. process any combat logic here whilst waiting to attack (blocking, strafing, dodging)
    // 3. if target moves out of combat range, switch to pursue target state
    // 4. if target is no longer present, switch to idle state

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

    // chance (%) of performing a spit attack
    [SerializeField] private int chanceToPerformSpitAtttack = 50;

    // [Header("Pivot")]
    // [SerializeField] protected bool enablePivot;

    [Header("Engagement Distance")]
    [SerializeField] public float maximumEngagementDistance = 3.1f; // the distance we have to be away from the target before we enter the pursue target state

    public override AIState Tick(AICharacterManager aiCharacter)
    {
        // if player is already performing action, return
        if(aiCharacter.isPerformingAction)
        {
            return this;
        }

        // ensures nav mesh is enabled
        if(!aiCharacter.navMeshAgent.enabled)
        {
            aiCharacter.navMeshAgent.enabled = true;
        }

        // if we want the ai character to face and turn towards its target when its outside its fov
        if(aiCharacter.aiCharacterCombatManager.enablePivot)
        {
            if(!aiCharacter.IsMoving)
            {
                // if character is outside of the viewing angle
                if(aiCharacter.aiCharacterCombatManager.viewableAngle < -30 || aiCharacter.aiCharacterCombatManager.viewableAngle > 30)
                {
                    // pivot
                    aiCharacter.aiCharacterCombatManager.PivotTowardsTarget(aiCharacter);
                }
            }
        }

        // rotate to face our target
        aiCharacter.aiCharacterCombatManager.RotateTowardsAgent(aiCharacter);

        // if our target is no longer present, switch back to idle state
        if(aiCharacter.aiCharacterCombatManager.currentTarget == null)
        {
            return SwitchState(aiCharacter, aiCharacter.idle);
        }

        // if we do not have an attack, get one
        if(!hasAttack)
        {
            GetNewAttack(aiCharacter);
        }
        else
        {
            // check recovery timer
            // pass attack to attack state
            aiCharacter.attack.currentAttack = chosenAttack;
            // roll for a combo chance
            // switch state
            return SwitchState(aiCharacter, aiCharacter.attack);
        }

        // if we are outside the combat engagement distance
        if(aiCharacter.aiCharacterCombatManager.distanceFromTarget > maximumEngagementDistance)
        {
            // roll for chance to perform spit attack
            if(RollForOutcomeChance(chanceToPerformSpitAtttack))
            {
                // perform spit attack
                return SwitchState(aiCharacter, aiCharacter.spitAttack);
            }

            // if spit attack is false, return to pursue state
            return SwitchState(aiCharacter, aiCharacter.pursueTarget);
        }

        // calculate path using nav mesh
        NavMeshPath path = new NavMeshPath();
        aiCharacter.navMeshAgent.CalculatePath(aiCharacter.aiCharacterCombatManager.currentTarget.transform.position, path);
        aiCharacter.navMeshAgent.SetPath(path);

        return this;
    }

    protected virtual void GetNewAttack(AICharacterManager aiCharacter)
    {
        // 1. sort through all possible attacks
        potentialAttacks = new List<AICharacterAttackAction>();

        // 2. remove attacks that can't be used in this situation (based on angle and distance)
        foreach(var potentialAttack in aiCharacterAttacks)
        {
            // check if we are too close to perform attack
            if(potentialAttack.minimumAttackDistance > aiCharacter.aiCharacterCombatManager.distanceFromTarget)
            {
                continue;
            }

            // check if we are too far to perform attack
            if(potentialAttack.maximumAttackDistance < aiCharacter.aiCharacterCombatManager.distanceFromTarget)
            {
                continue;
            }

            // check if the target is outside of the minimum field of view
            if(potentialAttack.minimumAttackAngle > aiCharacter.aiCharacterCombatManager.viewableAngle)
            {
                continue;
            }
            
            // check if the target is outside of the maximum field of view
            if(potentialAttack.maximumAttackAngle < aiCharacter.aiCharacterCombatManager.viewableAngle)
            {
                continue;
            }

            // 3. place remaining attacks into a list
            potentialAttacks.Add(potentialAttack);
        }

        // 4. pick an attack from remaining list randomly, based on weight
        if(potentialAttacks.Count <= 0)
        {
            // Debug.Log("NO ATTACKS");
            return;
        }

        // variable to hold attack action attack weight
        var totalWeight = 0;

        foreach(var attack in potentialAttacks)
        {
            // set total weight to attack action attack weight
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

    // roll based on given percentage
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

        //reset these state flags
        hasAttack = false;
        hasRolledForComboChance = false;
    }

}
