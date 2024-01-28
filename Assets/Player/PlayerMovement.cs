using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walking,
        Running,
        Crouching,
        Grabbed
    }

    public State state;

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

    public int struggleNeeded = 10;
    public float struggleTimer = 3.0f;
    List<float> struggleHistory = new List<float>();

    public Vector3[] checkpoint_positions = new Vector3[3];
    public int current_checkpoint = 0;

    public void Grab(GameObject source)
    {
        Debug.Log("Grabbed");
        struggleHistory = new List<float>();
        handleStateTransition(state, State.Grabbed);
        state = State.Grabbed;
        transform.SetParent(source.transform);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void Drop()
    {
        Debug.Log("Dropped");
        handleStateTransition(state, State.Idle);
        state = State.Idle;
        Vector3 position = transform.parent.position;
        transform.parent = null;
        transform.position = position;
        transform.GetComponent<Rigidbody>().isKinematic = true;
    }

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

        for (int i = 0; i < struggleHistory.Count; i++)
        {
            if (Time.time - struggleHistory[i] >= struggleTimer)
            {
                struggleHistory.RemoveRange(i, struggleHistory.Count - i);
                Debug.Log("Cull struggle");
                break;
            }
        }
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
                transform.position = transform.parent.position;
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
        if (state == State.Grabbed)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("struggle");
                struggleHistory.Add(Time.time);
            }

            if (struggleHistory.Count >= struggleNeeded)
            {
                Drop();
                return;
            }
        }
        else if (hasMovement)
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
