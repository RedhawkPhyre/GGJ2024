using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentAreaMusic : MonoBehaviour
{
    public AudioSource music;
    public AudioSource newMusic;
    // Start is called before the first frame update
    private void OnTriggerExit(Collider other)
    {
        music.enabled = false;
        newMusic.enabled = true;
    }
}
