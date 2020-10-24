using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleMovement : MonoBehaviour
{
    [SerializeField]
    private float accelerationForce = 10;
    [SerializeField]
    private float maxSpeed = 2;
    [SerializeField]
    private float jumpHeight = 10;
    [SerializeField]
    private PhysicMaterial stoppingMat, movingMat;

    private bool pressedJump;
    private new Rigidbody rigidbody;
    private Vector2 input;
    private new CapsuleCollider collider;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
    }
    
//if (axisH != 0) { movementVector.x = axisH* speed;}
//if (axisV != 0) { movementVector.z = axisV* speed; }
//if (axisH == 0) { movementVector.x -= (body.velocity.x* movementDamp); }
//if (axisV == 0) { movementVector.z -= (body.velocity.z* movementDamp); }
    private void FixedUpdate()
    {
        var inputDir = new Vector3(input.x, 0, input.y);
        inputDir = inputDir.normalized;
            rigidbody.AddForce(inputDir * accelerationForce);
        if (pressedJump == true)
        {
            rigidbody.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        pressedJump = Input.GetKeyDown(KeyCode.Space);
    }
}
