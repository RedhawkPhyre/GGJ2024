using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathing: MonoBehaviour
{
    private List<Node> pathing_nodes;
    void Start() {
        var all_nodes = FindObjectsOfType<Node>();
        pathing_nodes = new List<Node>(all_nodes);
        Debug.Log(pathing_nodes);
    }

    private Node GetClosestNode(Vector3 position) {
        Node closest = null;
        float closest_distance = float.PositiveInfinity;
        foreach (var node in pathing_nodes) {
            if (Vector3.Distance(position, node.transform.position) < closest_distance) {
                closest_distance = Vector3.Distance(position, node.transform.position);
                closest = node;
            }
        }
        return closest;
    }

    public List<Vector3> FindPath(Vector3 from, Vector3 to) {
        Dictionary<Node, float> distances = new Dictionary<Node, float>();
        Dictionary<Node, Node> previous_nodes = new Dictionary<Node, Node>();
        HashSet<Node> open_set = new HashSet<Node>();

        foreach (var node in pathing_nodes) {
            distances[node] = float.PositiveInfinity;
            previous_nodes[node] = null;
            open_set.Add(node);
        }
        Node start = GetClosestNode(from);
        distances[start] = 0.0f;

        while (open_set.Count > 0) {
            Node current = null;
            float min_distance = float.PositiveInfinity;
            foreach (var node in open_set) {
                if (distances[node] < min_distance) {
                    min_distance = distances[node];
                    current = node;
                }
            }

            open_set.Remove(current);
            foreach (var neighbor in current.connected_nodes) {
                if (!open_set.Contains(neighbor)) { continue; }
                float edge_distance = Vector3.Distance(current.transform.position, neighbor.transform.position);
                float path_distance = distances[current] + edge_distance;

                if (path_distance < distances[neighbor]) {
                    distances[neighbor] = path_distance;
                    previous_nodes[neighbor] = current;
                }
            }
        }

        List<Vector3> path = new List<Vector3>();
        Node processing = GetClosestNode(to);
        while (processing != start) {
            path.Add(processing.transform.position);
            processing = previous_nodes[processing];
        }
        path.Add(start.transform.position);
        path.Reverse();

        return path;
    }
}
