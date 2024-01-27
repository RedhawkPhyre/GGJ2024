using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSense : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SenseManager.GetSenseManager().RegisterSense(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
