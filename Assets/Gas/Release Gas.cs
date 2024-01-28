using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseGas : MonoBehaviour
{

    public Transform spawnpoint;
    public GameObject preFab;

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(preFab, spawnpoint.position, spawnpoint.rotation);
        Destroy(gameObject);
    }
}
