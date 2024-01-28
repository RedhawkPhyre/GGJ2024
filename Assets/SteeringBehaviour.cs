using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour : MonoBehaviour
{

    private Rigidbody rb;
    private Steering[] steerings;
    public float maxAcceleration = 10f;
    public float drag = 1f; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        steerings = GetComponents<Steering>();
        rb.drag = drag;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 accelaration = Vector3.zero;
        float rotation = 0f;
        foreach(Steering behaviour in steerings)
        {
            SteeringData steering = behaviour.GetSteering(this);
            accelaration += steering.linear;
            rotation += steering.angular;
        }

        if(accelaration.magnitude > maxAcceleration)
        {
            accelaration.Normalize();
            accelaration *= maxAcceleration;
        }

        rb.AddForce(accelaration);
        if(rotation != 0)
        {
            rb.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }
}
