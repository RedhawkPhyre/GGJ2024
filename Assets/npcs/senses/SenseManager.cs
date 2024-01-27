using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseManager : MonoBehaviour
{
    public GameObject sight_target;

    List<SightSense> sight_senses = new List<SightSense>();
    List<HearSense> hear_senses = new List<HearSense>();
    List<SmellSense> smell_senses = new List<SmellSense>();

    public void RegisterSense(SightSense sense)
    {
        sight_senses.Add(sense);
    }

    public void RegisterSense(HearSense sense)
    {
        hear_senses.Add(sense);
    }

    public void RegisterSense(SmellSense sense)
    {
        smell_senses.Add(sense);
    }

    public void SpawnNoise(Vector3 position)
    {
        AiDebugVars debug_vars = AiDebugVars.GetDebugVars();
        if (debug_vars && debug_vars.DrawSounds)
        {
        }

        foreach (var ear in hear_senses) {
            ear.Alert(position);
        }
    }

    public void SpawnScent(Vector3 position)
    {
        AiDebugVars debug_vars = AiDebugVars.GetDebugVars();
        if (debug_vars && debug_vars.DrawSmell)
        {
        }

        foreach (var nose in smell_senses) {
            nose.Alert(position);
        }
    }

    public static SenseManager GetSenseManager()
    {
        GameObject ai_object = GameObject.Find("GlobalAiState");
        if (!ai_object) { return null; }
        return ai_object.GetComponent<SenseManager>();
    }
}
