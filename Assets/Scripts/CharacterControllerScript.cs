using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    [SerializeField]
    private float jumpHeight = 10f;
    [SerializeField]
    private float doubleJumpScale = 1f;
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float sprintScale = 2;
    [SerializeField]
    private float gravityScale = 1f;
    [SerializeField]
    private float gravityIncrement = .01f;

    private bool hasDoubleJump;
    private bool isSprinting;
    private float origialGravity;
    private Vector3 moveDirection;

    public float MoveDirectionY
    {
        get { return moveDirection.y; }
        set { moveDirection.y = value;  }
    }

    CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveDirection = new Vector3(0f, 0f, 0f);
        origialGravity = gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        MovementCalculations();

        CheckForJumps();

        //ensures the player isn't constantly gaining downword momentum on the ground
        if (characterController.isGrounded)
        {
            gravityScale = origialGravity;
            if (moveDirection.y < -.5)
            {
                moveDirection.y -= moveDirection.y;
            }
        }
        //applies gravity once the player leaves the ground
        else
        {
            if (moveDirection.y < 0)
            {
                gravityScale += gravityIncrement;
            }
            moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);
        }

        Debug.Log("MoveDirection.Y = " + moveDirection.y);
        characterController.Move((moveDirection) * Time.deltaTime);
    }

    private void CheckForJumps()
    {
        //checks for jumps off the ground
        if (characterController.isGrounded)
        {
            hasDoubleJump = true;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpHeight;

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
        moveDirection = moveDirection * moveSpeed * CheckForSprint();
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

        if (Input.GetAxisRaw("Vertical") == 0f && Input.GetAxisRaw("Horizontal") == 0f)
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
}
