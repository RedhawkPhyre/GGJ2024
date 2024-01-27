using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathing: MonoBehaviour
{
    public LayerMask ground_layer;
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
            if (
                Vector3.Distance(position, node.transform.position) < closest_distance &&
                !DoesPathIntersect(position, node.transform.position)
            ) {
                closest_distance = Vector3.Distance(position, node.transform.position);
                closest = node;
            }
        }
        return closest;
    }

    private bool DoesPathIntersect(Vector3 a, Vector3 b)
    {
        Vector3 direction = Vector3.Normalize(b - a);
        float distance = Vector3.Distance(a, b);
        RaycastHit info;
        return Physics.SphereCast(a, 0.5f * 1.0f, direction, out info, distance, ~ground_layer);
    }

    public List<Vector3> RefinePath(Vector3 from, Vector3 to, in List<Vector3> path)
    {
        // Go through path, and adjust waypoints to allow for LOS movement
        path.Insert(0, from);
        path.Add(to);
        List<Vector3> refined_path = new List<Vector3>();
        bool first = true;
        for (int i = 0; i < path.Count - 1; i++)
        {
            for (int j = path.Count - 1; j > i; j--)
            {
                if (!DoesPathIntersect(path[i], path[j]))
                {
                    if (first)
                    {
                        refined_path.Add(path[i]);
                        first = false;
                    }
                    refined_path.Add(path[j]);
                    i = j - 1;
                    break;
                }
                
            }
        }

        return refined_path;
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
