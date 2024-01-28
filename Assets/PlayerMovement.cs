using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform orientation;

    // Run
    public float runSpeed; // 10 is ideal
    public float maxRun; // 5 is ideal
    public bool run;

    // Walk
    public float walkSpeed; // 0.05 is ideal
    public float maxWalk; // 3 is ideal
    public bool walk = false;

    // Crouch
    public float crouchSpeed; // 0.02 is ideal
    public float maxCrouch; // 1.5 is ideal
    public float crouchYScale;
    public float startYScale;
    public bool crouch = false;

    float horizontalInput;
    float verticalInput;

    //running sound
    public AudioSource squeakyRunAudio;
    public AudioSource runStopAudio;
    public bool moving = false;

    Vector3 moveDirection;

    Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
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
        // Update current positions
        movePlayer();

        //Play Sounds
        playRunSound();
    }

    private void getInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
            walk = false;
            crouch = false;
        }else if (Input.GetKey(KeyCode.LeftControl))
        {
            crouch = true;
            run = false;
            walk = false;
        }
        else
        {
            walk = true;
            crouch = false;
            run = false;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            moving = true;
        } else
        {
            moving = false;
        }
    }

    private void movePlayer()
    {
        // Generate move speed and direction
        moveDirection = orientation.right * horizontalInput + (orientation.forward * verticalInput);

        // Check if player is shift walking
        if (walk)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxWalk);
            rb.position += moveDirection * walkSpeed;
        }

        // Check if player is crouched
        if (crouch)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxCrouch);
            rb.position += moveDirection * crouchSpeed;
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }

        // Check if plater is running
        if(run)
        {
            rb.AddForce(moveDirection.normalized * runSpeed * 10f, ForceMode.Force);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxRun);

            //play run audio
            if(moving) squeakyRunAudio.enabled = true;
        } 
       
    }

    private void playRunSound()
    {
        if (run && moving)
        {
            squeakyRunAudio.enabled = true;
        } else
        {
            squeakyRunAudio.enabled = false;
            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                runStopAudio.PlayOneShot(runStopAudio.clip);
            }
        }
    }
}