using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSense : MonoBehaviour
{
    public Transform assigned_transform;
    public bool sees_target = false;
    public Vector3 target_position;
    public float range = 10.0f;

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
