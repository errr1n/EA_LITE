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

    public void UpdateAnimatorMovementParameters(float horizontalMovement, float verticalMovement)
    {
        // ACCESSING HUMANOID ANIMATOR PARAMETERS "HORIZONTAL" AND "VERTICAL"
        character.animator.SetFloat("Horizontal", horizontalMovement, 0.1f, Time.deltaTime);
        character.animator.SetFloat("Vertical", verticalMovement, 0.1f, Time.deltaTime);

        // IF ANIMATIONS DON'T LOOK SMOOTH, MAY NEED TO CLAMP HERE
    }
}
