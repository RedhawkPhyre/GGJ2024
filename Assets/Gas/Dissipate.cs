using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissipate : MonoBehaviour
{
    public float delay = 10;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
