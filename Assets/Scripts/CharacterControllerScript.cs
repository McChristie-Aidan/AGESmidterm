using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    [SerializeField]
    private float jumpHeight = 7;
    [SerializeField]
    private float moveSpeed = 10;

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

        moveDirection = new Vector3(xMovement * moveSpeed, 0f, yMovement);

        //if (Jump)
        //{
        //    moveDirection 
        //}
    }
}
