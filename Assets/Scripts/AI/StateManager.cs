using AI.States;
using UnityEngine;

namespace AI
{
    public class StateManager : MonoBehaviour
    {
        [field: SerializeField] public AttackState AttackState { get; private set; }
        [field: SerializeField] public ChaseState ChaseState { get; private set; }
        [field: SerializeField] public WanderState WanderState { get; private set; }

        [Space] [SerializeField] private State currentState; 
        private void Update()
        {
            RunStateMachine();
        }

        private void RunStateMachine()
        {
            var nextState = currentState?.RunCurrentState();
            if (nextState != null)
                SwitchToNextState(nextState);
        }

        private void SwitchToNextState(State nextState)
        {
            currentState = nextState;
        }
    }
}