using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotionManager : MonoBehaviour
{
    CharacterManager character;
    
    [Header("GROUND CHECK & JUMPING")]
    [SerializeField] float groundCheckSphereRadius = 1;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float gravityForce = -5.55f;

    [SerializeField] protected Vector3 yVelocity; // THIS IS THE FORCE AT WHICH OUR CHARACTER IS PULLED UP OR DOWN (JUMPING OR FALLING)
    [SerializeField] protected float groundedYVelocity = -20; // THE FORCE AT WHICH THE CHARACTER IS STICKING TO THE GROUND WHILE THEY ARE GROUNDED
    [SerializeField] protected float fallStartYVelocity = -5; // THE FORCE AT WHICH THE CHARACTER BEGINS TO FALL WHEN THEY BECOME UNGROUNDED (RISES AS THEY FALL LONGER)
    protected bool fallingVelocityHasBeenSet = false;
    protected float inAirTimer = 0;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    protected virtual void Update()
    {
        HandleGroundCheck();

        if(character.isGrounded)
        {
            // IF WE ARE NOT ATTEMPTING TO JUMP OR MOVE UPWARD
            if(yVelocity.y < 0)
            {
                inAirTimer = 0;
                fallingVelocityHasBeenSet = false;
                yVelocity.y = groundedYVelocity;
            }
        }
        else
        {
            // IF WE ARE NOT JUMPING, AND OUR FALLING VELOCITY HAS NOT BEEN SET
            if(!character.isJumping && !fallingVelocityHasBeenSet)
            {
                fallingVelocityHasBeenSet = true;
                yVelocity.y = fallStartYVelocity;
            }

            inAirTimer += Time.deltaTime;

            yVelocity.y += gravityForce * Time.deltaTime;

            character.characterController.Move(yVelocity * Time.deltaTime);
        }
    }

    protected virtual void HandleGroundCheck()
    {
        // CREATE CHECK SPHERE AT PLAYER POSITION WITH A RADIUS OF 1 AND LAYER MASK OF 0 (DEFAULT)
        character.isGrounded = Physics.CheckSphere(character.transform.position, groundCheckSphereRadius, groundLayer);
    }

    // DRAWS OUR GROUND CHECK SPHERE IN SCENE VIEW
    protected void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(character.transform.position, groundCheckSphereRadius);
    }
}
