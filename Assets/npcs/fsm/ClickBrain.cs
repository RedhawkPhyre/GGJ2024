using System.Collections.Generic;
using UnityEngine;

public class ClickBrain : AgentBrain
{
    public List<Vector3> path = null;
    public new void Update() {
        if (Input.GetMouseButtonUp(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                path = GetComponent<AStarPathing>().FindPath(transform.position, hit.point);
            }
        }
        base.Update();
    }
}
