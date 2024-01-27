using UnityEngine;

public class AiDebugVars : MonoBehaviour
{
    public bool DrawGizmos = true;
    public bool DrawSounds = true;
    public bool DrawSight = true;
    public bool DrawSmell = true;

    public static AiDebugVars GetDebugVars() {
        GameObject debug_object = GameObject.Find("AiDebugVars");
        if (!debug_object) { return null; }
        AiDebugVars debug_vars = debug_object.GetComponent<AiDebugVars>();
        return debug_vars;
    }
}

