using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class State : MonoBehaviour
    {
        protected NavMeshAgent Agent;
        protected EnemyController Controller;
        protected StateManager StateManager;
        
        public virtual State RunCurrentState()
        {
            return this;
        }

        protected void Awake()
        {
            Agent = GetComponentInParent<NavMeshAgent>();
            Controller = GetComponentInParent<EnemyController>();
            StateManager = GetComponentInParent<StateManager>();
        }
    }
}