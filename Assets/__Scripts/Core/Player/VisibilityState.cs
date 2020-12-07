using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player
{
    public class VisibilityState : MonoBehaviour
    {
        public enum State
        {
            Visible,
            VisibleLeft,
            VisibleRight,
            Hidden
        }

        public State currentState { get; private set; }

        private void Start()
        {
            currentState = State.Visible;
        }

        public void SetVisibilityState(State state)
        {
            currentState = state;
        }

        /// <summary>Checking if current state is equals to one of given states. Returning true if current state is State.Visible</summary>
        public bool CheckVisibility(params State[] visibleStates)
        {
            if (currentState == State.Visible) return true;

            foreach (State visibleState in visibleStates)
            {
                if (currentState == visibleState) return true;
            }
            return false;
        }
    }
}