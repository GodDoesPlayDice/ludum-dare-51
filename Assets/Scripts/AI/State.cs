using System;
using UnityEngine;

namespace AI
{
    public abstract class State : MonoBehaviour
    {
        protected EnemyController EnemyController;
        public abstract State RunCurrentState();

        private void Awake()
        {
            EnemyController = GetComponentInParent<EnemyController>();
        }
    }
}