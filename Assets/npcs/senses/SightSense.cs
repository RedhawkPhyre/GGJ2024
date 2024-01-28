using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSense : MonoBehaviour
{
    public bool sees_target = false;
    public Vector3 target_position;

    // Start is called before the first frame update
    void Start()
    {
        SenseManager.GetSenseManager().RegisterSense(this);
    }

    public bool GetBelievedPosition(out Vector3 target)
    {
        target = target_position;
        return sees_target;
    }
}
