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
        public void OnEnter(AgentBrain brain)
        {

        }

        public StateBase Think(AgentBrain brain)
        {
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
