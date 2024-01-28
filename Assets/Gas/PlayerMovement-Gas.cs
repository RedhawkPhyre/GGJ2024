using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum State
    {
        Idle,
        Walking,
        Running,
        Crouching,
        Grabbed
    }

    State state;

    public Transform orientation;

    public float runSpeed;
    public float walkSpeed;
    public float crouchSpeed;

    public float crouchYScale;
    public float startYScale;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        state = State.Idle;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        startYScale = transform.localScale.y;

    }

    // Update is called once per frame
    private void Update()
    {
        // Get current axis locations
        getInput();
    }

    private void FixedUpdate()
    {
        // Generate move speed and direction
        moveDirection = orientation.right * horizontalInput + (orientation.forward * verticalInput);
        float speed = 0.0f;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Grabbed:
                return;
            case State.Walking:
                speed = walkSpeed;
                break;
            case State.Running:
                speed = runSpeed;
                break;
            case State.Crouching:
                speed = crouchSpeed;
                break;
        }
        rb.position += moveDirection.normalized * speed * Time.deltaTime;
    }

    private void handleStateTransition(State old_state, State new_state)
    {
        if (new_state == State.Crouching)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
        }
        else if (old_state == State.Crouching)
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }

        if (new_state == State.Running)
        {
            GetComponent<Animator>().SetBool("sprint", true);
        }
        else if (old_state == State.Running)
        {
            GetComponent<Animator>().SetBool("sprint", false);
        }
    }

    private void getInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        bool hasMovement = horizontalInput != 0.0f || verticalInput != 0.0f;

        State oldState = state;
        if (hasMovement)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                state = State.Running;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                state = State.Crouching;
            }
            else
            {
                state = State.Walking;
            }
        }
        else if (state != State.Grabbed)
        {
            state = State.Idle;
        }

        if (oldState != state)
        {
            handleStateTransition(oldState, state);
        }
    }
}
