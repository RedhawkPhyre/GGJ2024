using UnityEngine;

namespace Fetus {
    public class Spawn: StateBase
    {
        float enter_time = Time.time;
        public StateBase Think(AgentBrain brain)
        {
            if (Time.time - enter_time >= 5.0f)
            {
                return new Chase();
            }
            return null;
        }
    }

    public class Chase: StateBase
    {
        GameObject player;
        public void OnEnter(AgentBrain agent)
        {
            player = ((FetusBrain)agent).player;
        }

        public StateBase Think(AgentBrain brain)
        {
            if (Vector3.Distance(brain.agent.transform.position, player.transform.position) < 5.0f)
            {
                return new Kill();
            }
            return null;
        }
    }

    public class Kill: StateBase
    {
        public StateBase Think(AgentBrain brain)
        {
            return null;
        }
    }
}

