using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseGas : MonoBehaviour
{

    public Transform spawnpoint;
    public GameObject preFab;
    public AudioSource popSound;
    public AudioClip clip;

    private void OnTriggerEnter(Collider other)
    {
        popSound.loop = false;
        popSound.PlayOneShot(clip);
        Instantiate(preFab, spawnpoint.position, spawnpoint.rotation);
        Destroy(gameObject);
    }
}
