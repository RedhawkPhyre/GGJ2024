using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPerspective : MonoBehaviour
{
    //Variables
    public float senseX;
    public float senseY;

    public Transform orientation;

    float xRotation;
    float yRotation; 

    // Start is called before the first frame update
    void Start()
    {
        // Lock mouse to camera perspective
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Mouse Input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senseX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senseY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        
    }
}
