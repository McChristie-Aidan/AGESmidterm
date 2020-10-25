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

    private void ChargeJump()
    {
        if (characterController.isGrounded == true && player.IsMoving() == false && IsCrouching == true)
        {
            chargeTime += chargeSpeed * Time.deltaTime;
            if (chargeTime > maxJumpHeight)
            {
                chargeTime = maxJumpHeight;
            }
            Debug.Log(chargeTime);
            if (Input.GetButtonDown("Jump"))
            {
                player.MoveDirectionY = chargeTime;
            }
        }
        else
        {
            chargeTime = 0f;
        }
    }

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
