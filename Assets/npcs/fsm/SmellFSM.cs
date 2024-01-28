using System.Collections.Generic;
using UnityEngine;

namespace Smeller {
    public class Observe: StateBase
    {
        float time_entered = Time.time;
        public StateBase Think(AgentBrain brain)
        {
            SmellBrain agent = (SmellBrain)brain;

            if (Time.time - time_entered >= agent.patrol_change_time)
            {
                return new Patrol();
            }
            return null;
        }
    }

    public class Patrol: StateBase
    {
        enum State
        {
            MovingToNode,
            AtNode,
        }
        State path_state;
        List<Vector3> path;
        public void OnEnter(AgentBrain brain)
        {
            GameObject[] nodes = GameObject.FindGameObjectsWithTag("Zone1PathNodes");
            GameObject target = nodes[Random.Range(0, nodes.Length)];

            path = brain.agent.GetComponent<AStarPathing>().FindPath(
                brain.agent.transform.position, target.transform.position
            );
            path = brain.agent.GetComponent<AStarPathing>().RefinePath(
                brain.agent.transform.position, target.transform.position, path
            );

            path_state = State.AtNode;
        }

        public StateBase Think(AgentBrain brain)
        {
            if (path_state == State.AtNode)
            {
                path.RemoveAt(0);
                if (path.Count == 0)
                {
                    brain.agent.GetComponent<AiController>().Stop();
                    return new Observe();
                }
                path_state = State.MovingToNode;
            }
            else
            {
                Vector3 direction = path[0] - brain.transform.position;
                Debug.Log("Patrol");
                Debug.Log(direction);
                Debug.Log(path[0]);
                Debug.Log(brain.transform.position);
                brain.agent.GetComponent<AiController>().Walk(direction);
                if (direction.magnitude <= 2.0f)
                {
                    path_state = State.AtNode;
                }
            }
            return null;
        }
    }

    public class Chase: StateBase
    {
        public StateBase Think(AgentBrain brain)
        {
            return null;
        }
    }

    public class Grab: StateBase
    {
        public StateBase Think(AgentBrain brain)
        {
            return null;
        }
    }

    public class Drag: StateBase
    {
        public StateBase Think(AgentBrain brain)
        {
            return null;
        }
    }

    public class DragFailed: StateBase
    {
        float time_entered = Time.time;
        public StateBase Think(AgentBrain brain)
        {
            SmellBrain agent = (SmellBrain)brain;

            if (Time.time - time_entered >= agent.failure_anger_time)
            {
                return new Patrol();
            }
            return null;
        }
    }

    public class DragSuccess: StateBase
    {
        float time_entered = Time.time;
        public StateBase Think(AgentBrain brain)
        {
            SmellBrain agent = (SmellBrain)brain;

            if (Time.time - time_entered >= agent.success_celebrate_time)
            {
                return new Patrol();
            }
            return null;
        }
    }
}
