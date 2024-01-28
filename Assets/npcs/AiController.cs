using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walking,
        Running,
        Grabbing
    }

    public float walk_acceleration = 1.0f;
    public float walk_speed = 4.0f;

    public float run_acceleration = 3.0f;
    public float run_speed = 10.0f;

    public float grab_acceleration = 7.0f;
    public float grab_speed = 12.0f;

    public State state = State.Idle;
    Vector3 state_direction;

    public GameObject agent;
    Rigidbody agent_body;

    public void Walk(Vector3 direction)
    {
        state = State.Walking;
        state_direction = direction;
    }

    public void Run(Vector3 direction)
    {
        state = State.Running;
        state_direction = direction;
    }

    public void Grab(Vector3 direction)
    {
        state = State.Grabbing;
        state_direction = direction;
    }

    public void Stop()
    {
        state = State.Idle;
    }

    void Start()
    {
        agent_body = agent.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 acceleration = Vector3.zero;
        float max_velocity = 0.0f;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Walking:
                acceleration = state_direction * walk_acceleration;
                max_velocity = walk_speed;
                break;
            case State.Running:
                acceleration = state_direction * run_acceleration;
                max_velocity = run_speed;
                break;
            case State.Grabbing:
                acceleration = state_direction * grab_acceleration;
                max_velocity = grab_speed;
                break;
        }

        agent_body.velocity += acceleration * Time.fixedDeltaTime;
        agent_body.velocity = Vector3.ClampMagnitude(agent_body.velocity, max_velocity);
    }
}
