using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchJump : MonoBehaviour
{
    //editor properties
    [SerializeField]
    private float crouchScale = .5f;
    [SerializeField]
    private float speedReduction = .3f;
    [SerializeField]
    private float chargeSpeed = 5;
    [SerializeField]
    private float maxJumpHeight = 40f;

    //code only variables
    private float chargeTime = 0f;

    //get sets
    public float CrouchScale { get { return crouchScale; } }

    public bool IsCrouching { get; private set; }

    public float SpeedReduction { get { return speedReduction; } }

    //objects
    CharacterController characterController;
    CharacterControllerScript player;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        player = GetComponent<CharacterControllerScript>();
    }

    void Update()
    {
        CrouchShrink();
        ChargeJump();
    }
    
    //charges up for a super jump if the player is stationary and crouching
    private void ChargeJump()
    {
        //runs if the player is on the ground, is not moving, and is in the crouching state 
        if (characterController.isGrounded == true && player.IsMoving() == false && IsCrouching == true)
        {
            Debug.Log(IsCrouching);
            chargeTime += chargeSpeed * Time.deltaTime;

            //makes sure the superjump wont be higher than the maximum jump height
            if (chargeTime > maxJumpHeight)
            {
                chargeTime = maxJumpHeight;
            }

            //Debug.Log("Current charge = " + chargeTime);

            //makes the player jump however high the current charge is
            if (Input.GetButtonDown("Jump"))
            {
                player.MoveDirectionY = chargeTime;
                player.createParticles();
            }
        }
        else
        {
            //resets the charge to 0 when the player isnt charging
            chargeTime = 0f;
        }
    }

    //smooshes the player when the crouch and then reverses the smooshing when the player stops crouching
    private void CrouchShrink()
    {
        if (Input.GetButtonDown("Crouch") && characterController.isGrounded)
        {
            this.gameObject.transform.localScale = new Vector3(1, crouchScale, 1);
            IsCrouching = true;
        }
        if (Input.GetButtonUp("Crouch"))
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            IsCrouching = false;
        }
    }
}
