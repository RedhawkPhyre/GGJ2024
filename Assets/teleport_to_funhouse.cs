using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport_to_funhouse : MonoBehaviour
{
    // Start is called before the first frame update
void OnTriggerEnter(Collider hit) {
GameObject.Find("Player").transform.position = GameObject.Find("door_exit").transform.position;

}

    
}
