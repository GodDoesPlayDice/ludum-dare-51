using System;
using UnityEngine;

namespace AI
{
    public abstract class State : MonoBehaviour
    {
        public abstract State RunCurrentState();
    }
}