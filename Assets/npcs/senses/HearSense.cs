using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearSense : MonoBehaviour
{
    public struct Noise {
        public Vector3 position;
        public float alert;
        public float time_created;
    }

    public float expire_time = 15.0f;
    [Range(0.0f, 1.0f)]
    public float sensitivity;
    public List<HearSense.Noise> noise_history = new List<HearSense.Noise>();

    public void Alert(Vector3 position)
    {
        float distance = Vector3.Distance(position, gameObject.transform.position);
        float alert = Mathf.Clamp01(1.0f / (sensitivity * sensitivity * distance * distance));
        noise_history.Add(new HearSense.Noise { alert=alert, position=position, time_created=Time.time });
    }

    public bool GetBelievedPosition(out Vector3 position)
    {
        // Heavily prefer newer sounds to old sounds
        Vector3 average_pos = Vector3.zero;
        float weight_sum = 0.0f;
        int index = 0;
        foreach (var noise in noise_history)
        {
            float weight = Mathf.Exp(-0.5f * index);
            weight_sum += weight;
            average_pos += noise.position / weight;
            index += 1;
        }

        position = average_pos / weight_sum;
        if (weight_sum == 0.0f)
        {
            return false;
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        SenseManager.GetSenseManager().RegisterSense(this);
    }

    void Update()
    {
        int destroy_from_index = 0;
        foreach (var sound in noise_history)
        {
            if (Time.time - sound.time_created >= expire_time) {
                // destroy!
                break;
            }
            destroy_from_index += 1;
        }

        while (destroy_from_index < noise_history.Count)
        {
            noise_history.RemoveAt(destroy_from_index);
        }
    }
}
