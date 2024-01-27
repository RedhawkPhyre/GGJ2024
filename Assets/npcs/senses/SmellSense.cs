using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellSense : MonoBehaviour
{
    public struct Scent {
        public Vector3 position;
        public float time_created;
    }

    public float expire_time = 15.0f;
    [Range(0.0f, 1.0f)]
    public float sensitivity;
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
        int destroy_from_index = 0;
        foreach (var scent in scent_history)
        {
            if (Time.time - scent.time_created >= expire_time) {
                // destroy!
                break;
            }
            destroy_from_index += 1;
        }

        while (destroy_from_index < scent_history.Count)
        {
            scent_history.RemoveAt(destroy_from_index);
        }
    }
}
