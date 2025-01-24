using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUtilityManager : MonoBehaviour
{
    public static WorldUtilityManager instance;

    [Header("Layers")]
    [SerializeField] LayerMask characterLayers;
    [SerializeField] LayerMask enviroLayers;

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

    public LayerMask GetCharacterLayers()
    {
        return characterLayers;
    }

    public LayerMask GetEnviroLayers()
    {
        return enviroLayers;
    }

    public bool CanIDamageThsTarget(CharacterGroup attackingCharacter, CharacterGroup targetCharacter)
    {
        if(attackingCharacter == CharacterGroup.Friendly)
        {
            switch (targetCharacter)
            {
                case CharacterGroup.Friendly:
                    return false;
                case CharacterGroup.Enemy:
                    return true;
                default:
                    break;
            }
        }
        else if(attackingCharacter == CharacterGroup.Enemy)
        {
            switch (targetCharacter)
            {
                case CharacterGroup.Friendly:
                    return true;
                case CharacterGroup.Enemy:
                    return false;
                default:
                    break;
            }
        }

        return false;
    }

    public float GetAngleOfTarget(Transform characterTransform, Vector3 targetsDirection)
    {
        targetsDirection.y = 0;
        float viewableAngle = Vector3.Angle(characterTransform.forward, targetsDirection);
        Vector3 cross = Vector3.Cross(characterTransform.forward, targetsDirection);

        if(cross.y < 0)
        {
            viewableAngle = -viewableAngle;
        }

        return viewableAngle;
    }
}
