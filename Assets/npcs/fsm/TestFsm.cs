using UnityEngine;

namespace AgentTest {
    public class Initial: StateBase
    {
        public StateBase Think(AgentBrain brain)
        {
            ClickBrain agent = (ClickBrain)brain;
            if (agent.path != null && agent.path.Count > 0) {
                return new FollowPath();
            }
            SmellSense nose = agent.gameObject.GetComponent<SmellSense>();
            HearSense ears = agent.gameObject.GetComponent<HearSense>();

            Vector3 p;
            if (ears.GetBelievedPosition(out p))
            {
                return new Pursue();
            }

            if (nose.GetBelievedPosition(out p) && nose.scent_history.Count >= 5)
            {
                return new Sniff();
            }

            return null;
        }
    }

    public class FollowPath: StateBase
    {
        int current_path_index = 0;
        public StateBase Think(AgentBrain brain)
        {
            ClickBrain agent = (ClickBrain)brain;

            Vector3 target = agent.path[current_path_index];
            target.y = agent.agent.transform.position.y;
            Vector3 direction = Vector3.Normalize(target - agent.agent.transform.position);

            agent.agent.transform.position += direction * 1.0f / 60.0f;
            if (Vector3.Distance(agent.agent.transform.position, target) <= 1.0) {
                current_path_index += 1;
            }

            if (current_path_index == agent.path.Count) {
                agent.path = null;
                Debug.Log("at target");
                return new Initial();
            }
            return null;
        }
    }

    public class Pursue: StateBase
    {
        public StateBase Think(AgentBrain brain)
        {
            HearSense ears = brain.agent.gameObject.GetComponent<HearSense>();
            Vector3 believed_position = Vector3.zero;
            if (!ears.GetBelievedPosition(out believed_position))
            {
                return new Initial();
            }
            GameObject.Find("TestBall").transform.position = believed_position;
            return null;
        }
    }

    public class Sniff: StateBase
    {
        public StateBase Think(AgentBrain brain)
        {
            SmellSense ears = brain.agent.gameObject.GetComponent<SmellSense>();
            Vector3 believed_position = Vector3.zero;
            if (!ears.GetBelievedPosition(out believed_position))
            {
                return new Initial();
            }
            GameObject.Find("TestBall").transform.position = believed_position;
            return null;
        }
    }
}

