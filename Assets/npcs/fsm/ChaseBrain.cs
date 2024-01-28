using UnityEngine;

public class ChaseBrain : AgentBrain
{
    public bool within_range = false;
    public GameObject target;
    private void OnTriggerEnter(Collider other)
    {
        within_range = true;
    }
    private void OnTriggerExit(Collider other)
    {
        within_range = false;
    }
}
