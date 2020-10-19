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

    private new Rigidbody rigidbody;
    private Vector2 input;
    private new CapsuleCollider collider;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        var inputDir = new Vector3(input.x, 0, input.y);
        inputDir = inputDir.normalized;
        if (rigidbody.velocity.magnitude < maxSpeed)
        {
            rigidbody.AddForce(inputDir * accelerationForce, ForceMode.Acceleration);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }
}
