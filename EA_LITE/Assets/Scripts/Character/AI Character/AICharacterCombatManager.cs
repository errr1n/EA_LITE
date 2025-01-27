using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterCombatManager : CharacterCombatManager
{
    [Header("Target Information")]
    [SerializeField] public float viewableAngle;
    public Vector3 targetsDirection;

    [Header("Detection")]
    [SerializeField] float detectionRadius = 15;
    [SerializeField] public float minimumFOV = -35;
    [SerializeField] public float MaximumFOV = 35;

    public void FindATargetViaLineOfSight(AICharacterManager aiCharacter)
    {
        if(currentTarget != null)
        {
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(aiCharacter.transform.position, detectionRadius, WorldUtilityManager.instance.GetCharacterLayers());

        for(int i = 0; i < colliders.Length; i++)
        {
            CharacterManager targetCharacter = colliders[i].transform.GetComponent<CharacterManager>();

            if(targetCharacter == null)
            {
                continue;
            }

            if(targetCharacter == aiCharacter)
            {
                continue;
            }
                
            if(targetCharacter.isDead)
            {
                continue;
            }

            // can i attack this character
            if(WorldUtilityManager.instance.CanIDamageThsTarget(aiCharacter.characterGroup, targetCharacter.characterGroup))
            {
                // IF A POTENTIAL TARGET IS FOUND, IT HAS TO BE IN FRONT OF US
                Vector3 targetDirection = targetCharacter.transform.position - aiCharacter.transform.position;
                float angleOfPotentialTarget = Vector3.Angle(targetDirection, aiCharacter.transform.forward);

                if(angleOfPotentialTarget > minimumFOV && angleOfPotentialTarget < MaximumFOV)
                {
                    // lastly, check for environmental blockage
                    if(Physics.Linecast(aiCharacter.characterCombatManager.lockOnTransform.position, 
                    targetCharacter.characterCombatManager.lockOnTransform.position, 
                    WorldUtilityManager.instance.GetEnviroLayers()))
                    {
                        Debug.DrawLine(aiCharacter.characterCombatManager.lockOnTransform.position, targetCharacter.characterCombatManager.lockOnTransform.position);
                        // Debug.Log("BLOCKED");
                    }
                    else
                    {
                        // target direction is current target position - the position of the chasing character
                        targetsDirection = targetCharacter.transform.position - transform.position;
                        viewableAngle = WorldUtilityManager.instance.GetAngleOfTarget(transform, targetsDirection);
                        // Debug.Log(viewableAngle);
                        
                        aiCharacter.characterCombatManager.SetTarget(targetCharacter);
                        PivotTowardsTarget(aiCharacter);
                        // Debug.Log("pivot");
                    }
                }
            }
        }
    }

    public void PivotTowardsTarget(AICharacterManager aiCharacter)
    {
        // PLAY A PIVOT ANIMATION DEPENDING ON VIEWABLE ANGLE OF CURRENT TARGET
        // if(aiCharacter.isPerformingAction)
        // {
        //     return;
        // }

        // if(viewableAngle >= 20 && viewableAngle <= 60)
        // {
        //     aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_45", true);
        // }

        // else if(viewableAngle <= -20 && viewableAngle >= -60)
        // {
        //     aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_45", true);
        // }

        // if(viewableAngle >= 61 && viewableAngle <= 110)
        // {
        //     aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_90", true);
        // }

        // else if(viewableAngle <= -61 && viewableAngle >= -110)
        // {
        //     aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_90", true);
        // }

        // if(viewableAngle >= 111 && viewableAngle <= 145)
        // {
        //     //should be 135
        //     aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_180", true);
        // }

        // else if(viewableAngle <= -111 && viewableAngle >= -145)
        // {
        //     aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_180", true);
        // }

        // if(viewableAngle >= 146 && viewableAngle <= 180)
        // {
        //     aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_180", true);
        // }

        // else if(viewableAngle <= -146 && viewableAngle >= -180)
        // {
        //     aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_180", true);
        // }

        // Debug.Log(viewableAngle);
    }
}
