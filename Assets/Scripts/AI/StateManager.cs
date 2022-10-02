using System;
using AI.States;
using UnityEngine;

namespace AI
{
    public class StateManager : MonoBehaviour
    {
        [field: SerializeField] public AttackState AttackState { get; private set; }
        [field: SerializeField] public WanderState WanderState { get; private set; }
        [field: SerializeField] public ChaseState ChaseState { get; private set; }

        [field: SerializeField] public IdleState IdleState { get; private set; }

        [field: Space] [field: SerializeField] public State CurrentState { get; private set; }

        private State _prevState;
        public event Action<State> OnStateChange;

        private void Update()
        {
            RunStateMachine();
        }

        private void RunStateMachine()
        {
            var nextState = CurrentState?.RunCurrentState();
            _prevState = CurrentState;
            if (nextState != null)
                CurrentState = nextState;

            if (CurrentState != _prevState)
                OnStateChange?.Invoke(CurrentState);
        }
    }
}