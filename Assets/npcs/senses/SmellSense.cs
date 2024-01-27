using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellSense : MonoBehaviour
{
    public struct Scent {
        public Vector3 position;
        public float time_created;
    }

    private float _sensitivity = 1.0f;
    public float sensitivity {
        get { return _sensitivity; }
        set { _sensitivity = Mathf.Clamp01(value); }
    }

    public List<SmellSense.Scent> scent_history = new List<SmellSense.Scent>();

    public void Alert(Vector3 position)
    {
        scent_history.Add(new SmellSense.Scent { time_created=Time.time, position=position });
    }
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
