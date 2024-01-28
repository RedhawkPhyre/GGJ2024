using UnityEngine;

public class AgentBrain : MonoBehaviour
{
    public enum StartStates {
        AgentTest
    }

    private StateBase GetStartState() {
        switch (start_state) {
            case StartStates.AgentTest:
                return new AgentTest.Initial();
        }
        return null;
    }

    protected StateBase current_state;
    [SerializeField] public StartStates start_state;
    public GameObject agent = null;

    void Start() {
        if (agent == null) {
            agent = gameObject;
        }
        current_state = GetStartState();
        current_state.OnEnter(this);
    }

    public void Update() {
        StateBase next_state = current_state.Think(this);
        if (next_state != null) {
            current_state.OnExit(this);
            current_state = next_state;
            current_state.OnEnter(this);
        }
    }
}
