using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    //editor variables
    [SerializeField]
    private float jumpHeight = 10f;
    [SerializeField]
    private float doubleJumpScale = 1f;
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float sprintScale = 2;
    [SerializeField]
    private float gravityScale = 6f;
    [SerializeField]
    private float jumpingLowGravity = 4f;
    //technical debt needs refactoring 
    [SerializeField]
    private ParticleSystem particleSource;
    [SerializeField]
    private float fallingParticleThreshold = -50f;

    //script only variables 
    private bool hasDoubleJump;
    private bool isSprinting;
    private Vector3 moveDirection;
    private float originalGravity;

    //get sets
    public float MoveDirectionY
    {
        get { return moveDirection.y; }
        set { moveDirection.y = value;  }
    }

    //objects
    CharacterController characterController;
    CrouchJump crouchJump;
    //needs to be refactored
    ParticleSystem partSys;

    private void Start()
    {
        //gets the other components we need
        characterController = GetComponent<CharacterController>();
        crouchJump = GetComponent<CrouchJump>();
        // needs to be refactored
        partSys = particleSource.GetComponent<ParticleSystem>();

        //initializes variables
        moveDirection = new Vector3(0f, 0f, 0f);
        originalGravity = gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        MovementCalculations();

        if (crouchJump.IsCrouching == false)
        {
            CheckForJumps();
        }

        if (characterController.isGrounded && isSprinting == true)
        {
            particleSource.Play();
        }

        ApplyGravity();
        
        //Debug.Log("MoveDirection.Y = " + moveDirection.y);
        characterController.Move((moveDirection) * Time.deltaTime);
    }

    private void CheckForJumps()
    {
        //checks for jumps off the ground
        if (characterController.isGrounded && crouchJump.IsCrouching != true)
        {
            hasDoubleJump = true;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpHeight;
                createParticles();
            }
        }

        //checks for double jumps
        if (characterController.isGrounded == false)
        {
            if (hasDoubleJump)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    hasDoubleJump = false;
                    moveDirection.y = jumpHeight * doubleJumpScale;
                    createParticles();
                }
            }
        }
    }
    private void MovementCalculations()
    {
        //takes in input to know where the player is moving
        float xMovement = Input.GetAxisRaw("Horizontal");
        float yMovement = Input.GetAxisRaw("Vertical");

        //finds which way the camera is facing.
        Vector3 cameraFlattenedForward = Camera.main.transform.forward;
        cameraFlattenedForward.y = 0;
        var cameraRotation = Quaternion.LookRotation(cameraFlattenedForward);

        //technical debt. stores y direction so we can use it later after reseting the ground movement
        float yStorage = moveDirection.y;

        //makes a vector 3 for player movement
        moveDirection = new Vector3(xMovement, 0f, yMovement);
        //normalizes the move vector so moving diagonally is the same speed
        moveDirection = moveDirection.normalized;
        //multiplies move vector by move speed to get the desired speed
        if (crouchJump.IsCrouching)
        {
            isSprinting = false;
            moveDirection = moveDirection * moveSpeed * crouchJump.SpeedReduction;
        }
        else
        {
            moveDirection = moveDirection * moveSpeed * CheckForSprint();
        }
        //adds our y back onto the move vector so we can retain momentum
        moveDirection.y = yStorage;
        //rotates movement relevant to the camera
        moveDirection = cameraRotation * moveDirection;
    }

    private float CheckForSprint()
    {
        if (Input.GetButtonDown("Sprint") && characterController.isGrounded)
        {
            isSprinting = true;
        }
        //technical debt need to refactor this
        if (IsMoving() != true)
        {
            isSprinting = false;
        }

        if (isSprinting)
        {
            return sprintScale;
        }
        else
        {
            return 1;
        }
    }
    private void ApplyGravity()
    {
        //ensures the player isn't constantly gaining downword momentum on the ground
        if (characterController.isGrounded)
        {
            gravityScale = originalGravity;
            if (moveDirection.y < -.5)
            {
                moveDirection.y -= moveDirection.y;
            }
        }
        //applies gravity once the player leaves the ground
        else
        {
            gravityScale = originalGravity;
            if (moveDirection.y > 0 && Input.GetButton("Jump") == true)
            {
                gravityScale = jumpingLowGravity;
            }
            if (moveDirection.y < 0)
            {
                if (moveDirection.y < fallingParticleThreshold)
                {
                    particleSource.Play();
                }
            }
            moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);
        }
    }
    //technical debt maybe refactor this into a utility script
    public bool IsMoving()
    {
        if (Input.GetAxisRaw("Vertical") != 0f || Input.GetAxisRaw("Horizontal") != 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //technical debt this should be its own class
    public void createParticles()
    {
        //partSys.Stop();

        //var particleDuration = partSys.main;
        //particleDuration.duration = duration;

        partSys.Play();
    }
}
