using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform orientation;
    public float moveSpeed;
    public bool walk = false; 

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    // Update is called once per frame
    private void Update()
    {
        // Get current axis locations
        getInput();
    }

    private void FixedUpdate()
    {
        // Update current positions
        movePlayer();
    }

    private void getInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void movePlayer()
    {
        moveDirection = orientation.right * horizontalInput + (orientation.forward * verticalInput);

        // Check if player is shift walking
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 2;
            walk = true;
        }
        else
        {
            moveSpeed = 5;
            walk = false;
        }

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}