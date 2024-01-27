using UnityEngine;

namespace AgentTest {
    public class Initial: StateBase
    {
        public StateBase Think(AgentBrain agent)
        {
            Debug.Log("foo!");
            return null;
        }
    }
}

