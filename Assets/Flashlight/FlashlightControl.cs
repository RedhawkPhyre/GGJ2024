using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public bool flashlightOn;
    public GameObject flashlight;
    // Start is called before the first frame update
    void Start()
    {
        flashlight.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            if (!flashlightOn)
            {
                flashlight.SetActive(true);
                flashlightOn = true;

            }
            else
            {
                flashlight.SetActive(false);
                flashlightOn = false;
            }
        }
    }

}
