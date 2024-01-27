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

            if (ears.noise_history.Count >= 2)
            {
                return new Pursue();
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
        SmellSense nose;
        HearSense ears;

        Vector3 believed_position;

        void OnEnter(AgentBrain brain)
        {
            ClickBrain agent = (ClickBrain)brain;
            nose = agent.gameObject.GetComponent<SmellSense>();
            ears = agent.gameObject.GetComponent<HearSense>();

            believed_position = Vector3.zero;
            foreach (var sound in ears.noise_history)
            {
                believed_position += sound.position;
            }
            believed_position /= ears.noise_history.Count;
        }

        public StateBase Think(AgentBrain brain)
        {
            if (ears.noise_history.Count < 2)
            {
                return new Initial();
            }

            believed_position = Vector3.zero;
            foreach (var sound in ears.noise_history)
            {
                believed_position += sound.position;
            }
            believed_position /= ears.noise_history.Count;
            return null;
        }
    }
}

