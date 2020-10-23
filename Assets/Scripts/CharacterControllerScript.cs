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
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        float yMovement = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(xMovement * moveSpeed, moveDirection.y, yMovement * moveSpeed);

        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpHeight;
            }
        }
        

        moveDirection.y += (Physics.gravity.y * gravityScale);
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
