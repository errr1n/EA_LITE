using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager character;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
    {
        // ACCESSING HUMANOID ANIMATOR PARAMETERS "HORIZONTAL" AND "VERTICAL"
        character.animator.SetFloat("Horizontal", horizontalValue);
        character.animator.SetFloat("Vertical", verticalValue);

        // IF ANIMATIONS DON'T LOOK SMOOTH, MAY NEED TO CLAMP HERE
    }
}
