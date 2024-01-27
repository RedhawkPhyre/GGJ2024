using UnityEngine;

namespace ChaseTest {
    public class Idle: StateBase
    {
        public StateBase Think(AgentBrain brain)
        {
            ChaseBrain agent = (ChaseBrain)brain;
            if (agent.within_range)
            {
                return new ChaseTest.Grab();
            }
            return null;
        }
    }

    public class Grab: StateBase
    {
        public void OnEnter(AgentBrain brain)
        {
            ChaseBrain agent = (ChaseBrain)brain;
            agent.target.GetComponent<PlayerMovement>().Grab(agent.agent);
        }

        public StateBase Think(AgentBrain brain)
        {
            brain.agent.transform.position += new Vector3(0, 10, 0) * Time.deltaTime;
            return null;
        }
    }
}

