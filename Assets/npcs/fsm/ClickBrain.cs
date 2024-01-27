using System.Collections.Generic;
using UnityEngine;

public class ClickBrain : AgentBrain
{
    public List<Vector3> path = null;
    bool GetRayHit(out Vector3 hit_pos) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            hit_pos = hit.point;
            return true;
        }
        hit_pos = Vector3.zero;
        return false;
    }
    public new void Update() {
        SenseManager sense_manager = SenseManager.GetSenseManager();
        if (Input.GetMouseButtonUp(0)) {
            Vector3 point;
            if (GetRayHit(out point)) {
                Debug.Log("Path to");
                path = GetComponent<AStarPathing>().FindPath(transform.position, point);
                path = GetComponent<AStarPathing>().RefinePath(transform.position, point, path);
            }
        } else if (Input.GetKeyUp(KeyCode.Alpha1)) {
            Vector3 point;
            if (GetRayHit(out point)) {
                if (sense_manager)
                {
                    Debug.Log("Spawn Scent");
                    sense_manager.SpawnScent(point);
                }
                else 
                {
                    Debug.Log("Cant spawn Scent: No sense_manager");
                }
            }
        } else if (Input.GetKeyUp(KeyCode.Alpha2)) {
            Vector3 point;
            if (GetRayHit(out point)) {
                if (sense_manager)
                {
                    Debug.Log("Spawn Noise");
                    sense_manager.SpawnNoise(point);
                } 
                else 
                {
                    Debug.Log("Cant spawn Noise: No sense_manager");
                }
            }
        } else if (Input.GetKeyUp(KeyCode.Alpha3)) {
            Vector3 point;
            if (GetRayHit(out point)) {
                Debug.Log("Spawn Sight");
            }
        }
        base.Update();
    }
}
