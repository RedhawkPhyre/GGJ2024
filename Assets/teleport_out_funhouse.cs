using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport_out_funhouse : MonoBehaviour
{
    // Start is called before the first frame update
void OnTriggerEnter(Collider hit) {
GameObject.Find("Player").transform.position = GameObject.Find("wheel_enter").transform.position;
GameObject.Find("Player").transform.rotation = GameObject.Find("wheel_enter").transform.rotation;

}

    
}
