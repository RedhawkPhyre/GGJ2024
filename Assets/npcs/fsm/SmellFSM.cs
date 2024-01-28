using System.Collections.Generic;
using UnityEngine;

namespace Smeller {
    class Utils
    {
        public static Vector3 GetTargetPos(AgentBrain brain)
        {
            SightSense eyes = brain.agent.GetComponent<SightSense>();
            HearSense ears = brain.agent.GetComponent<HearSense>();
            SmellSense nose = brain.agent.GetComponent<SmellSense>();

            float weight_sum = 0.0f;
            Vector3 target = Vector3.zero;
            Vector3 believed_position;
            if (eyes.GetBelievedPosition(out believed_position))
            {
                target += 100.0f * believed_position;
                weight_sum += 100.0f;
            }

            if (ears.GetBelievedPosition(out believed_position))
            {
                target += 3.0f * believed_position;
                weight_sum += 3.0f;
            }

            if (nose.GetBelievedPosition(out believed_position))
            {
                target += 6.0f * believed_position;
                weight_sum += 6.0f;
            }
            target /= weight_sum;
            return target;
        }
    }
    public class Observe: StateBase
    {
        float time_entered = Time.time;
        public StateBase Think(AgentBrain brain)
        {
            SmellBrain agent = (SmellBrain)brain;

            SightSense eyes = brain.agent.GetComponent<SightSense>();
            HearSense ears = brain.agent.GetComponent<HearSense>();
            SmellSense nose = brain.agent.GetComponent<SmellSense>();

            Vector3 believed_position;
            if (
                eyes.GetBelievedPosition(out believed_position) ||
                ears.GetBelievedPosition(out believed_position) ||
                nose.GetBelievedPosition(out believed_position)
            )
            {
                return new Chase();
            }

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
            HearSense ears = brain.agent.GetComponent<HearSense>();
            SmellSense nose = brain.agent.GetComponent<SmellSense>();
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
        Vector3 target;
        float time_at_target = -1.0f;
        public void OnEnter(AgentBrain brain)
        {
            target = Utils.GetTargetPos(brain);
        }

        public StateBase Think(AgentBrain brain)
        {
            SightSense eyes = brain.agent.GetComponent<SightSense>();
            HearSense ears = brain.agent.GetComponent<HearSense>();
            SmellSense nose = brain.agent.GetComponent<SmellSense>();

            float distance_to_target = (brain.agent.transform.position - target).magnitude;
            Vector3 believed_position;
            if (
                distance_to_target <= 2.0f &&
                !eyes.GetBelievedPosition(out believed_position) &&
                !ears.GetBelievedPosition(out believed_position) &&
                !nose.GetBelievedPosition(out believed_position)
            ) {
                if (time_at_target < 0.0f)
                {
                    time_at_target = Time.time;
                } else if (Time.time - time_at_target >= 5.0f)
                {
                    return new Observe();
                }
            }

            target += 5.0f * Utils.GetTargetPos(brain);
            target /= 6.0f;

            Vector3 direction = brain.agent.transform.position - target;
            brain.agent.GetComponent<AiController>().Run(direction);

            Vector3 player_pos;
            if (eyes.GetBelievedPosition(out player_pos))
            {
                if ((player_pos - brain.agent.transform.position).magnitude <= 10.0f)
                {
                    return new Grab();
                }
            }

            return null;
        }
    }

    public class Grab: StateBase
    {
        public void OnEnter(AgentBrain brain)
        {
            Vector3 direction = brain.agent.transform.position - SenseManager.GetSenseManager().sight_target.transform.position;
            brain.agent.GetComponent<AiController>().Grab(direction);
        }

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
