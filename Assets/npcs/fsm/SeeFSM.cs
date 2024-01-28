using UnityEngine;

namespace Seer
{
    public class Idle: StateBase
    {
        float time_entered = Time.time;
        public StateBase Think(AgentBrain brain)
        {
            EyeBrain agent = (EyeBrain)brain;

            if (Time.time - time_entered >= agent.patrol_change_time)
            {
                return new Patrol();
            }
            return null;
        }
    }

    public class Patrol: StateBase
    {
        float time_entered = Time.time;
        public StateBase Think(AgentBrain brain)
        {
            EyeBrain agent = (EyeBrain)brain;

            if (Time.time - time_entered >= agent.patrol_change_time)
            {
                return new Idle();
            }
            return null;
        }
    }

    public class Investigate: StateBase
    {
        public StateBase Think(AgentBrain brain)
        {
            return null;
        }
    }

    public class Cooldown: StateBase
    {
        float time_entered = Time.time;
        public StateBase Think(AgentBrain brain)
        {
            EyeBrain agent = (EyeBrain)brain;
            if (Time.time - time_entered >= agent.chase_cooldown_time)
            {
                return new Patrol();
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
            EyeBrain agent = (EyeBrain)brain;

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
            EyeBrain agent = (EyeBrain)brain;

            if (Time.time - time_entered >= agent.success_celebrate_time)
            {
                return new Patrol();
            }
            return null;
        }
    }
}
