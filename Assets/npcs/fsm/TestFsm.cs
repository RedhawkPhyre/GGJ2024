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
}

