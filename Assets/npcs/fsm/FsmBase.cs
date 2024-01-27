public interface StateBase
{
    public virtual void OnEnter(AgentBrain agent) {}
    public virtual void OnExit(AgentBrain agent) {}
    public virtual StateBase Think(AgentBrain agent) { return null; }
}
