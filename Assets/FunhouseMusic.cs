using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunhouseMusic : MonoBehaviour
{
    public AudioSource houseMusic;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        houseMusic.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        houseMusic.enabled = false;
    }
}
