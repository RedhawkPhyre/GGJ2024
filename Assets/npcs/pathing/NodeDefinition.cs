using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> editor_connected_nodes = new List<Node>();
    public HashSet<Node> connected_nodes = new HashSet<Node>();

    void Start() {
        foreach (var node in editor_connected_nodes) {
            if (node != null) {
                node.connected_nodes.Add(this);
                connected_nodes.Add(node);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.2f);
        Gizmos.color = Color.blue;
        foreach (var node in editor_connected_nodes)
        {
            if (node != null) {
                Gizmos.DrawLine(node.transform.position, transform.position);
            }
        }
    }
}
