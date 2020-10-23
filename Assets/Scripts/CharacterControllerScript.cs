using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    [SerializeField]
    private float jumpHeight = 10f;
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float gravityScale = 1f;

    private Vector3 moveDirection;

    CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveDirection = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //takes in input to know where the player is moving
        float xMovement = Input.GetAxisRaw("Horizontal");
        float yMovement = Input.GetAxisRaw("Vertical");

        //Camera.main.transform

        //technical debt. stores y direction so we can use it later after reseting the ground movement
        float yStorage = moveDirection.y;

        //makes a vector 3 for player movement
        moveDirection = new Vector3(xMovement, 0f, yMovement);
        //normalizes the move vector so moving diagonally is the same speed
        moveDirection = moveDirection.normalized;
        //multiplies move vector by move speed to get the desired speed
        moveDirection = moveDirection * moveSpeed;
        //adds our y back onto the move vector so we can retain momentum
        moveDirection.y = yStorage;

        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpHeight;
            }
        }
        

        moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
