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
    public float smell_radius = 5.0f;
    [Range(0.0f, 1.0f)]
    public float sensitivity;
    public List<SmellSense.Scent> scent_history = new List<SmellSense.Scent>();

    public void Alert(Vector3 position)
    {
        scent_history.Add(new SmellSense.Scent { time_created=Time.time, position=position });
    }

    public bool GetBelievedPosition(out Vector3 position)
    {
        // I am so sorry for this var name. its 5am and i cant be bothered
        // Its the smell that most fits the parameters:
        //  Within radius, and newest
        // this is so the agent will track the player, always heading toward the newest pos,
        // but never instantly tracking the player
        float most_fitting_time = float.PositiveInfinity;
        Vector3 most_fitting_smell = Vector3.zero;
        // Find newest smell within the radius, otherwise find the closest smell
        foreach (var smell in scent_history)
        {
            float distance = Vector3.Distance(smell.position, gameObject.transform.position);
            if (distance < smell_radius && smell.time_created < most_fitting_time)
            {
                most_fitting_time = Time.time;
                most_fitting_smell = smell.position;
            }
        }

        position = most_fitting_smell;
        if (float.IsInfinity(most_fitting_time))
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
