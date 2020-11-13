using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Player
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

        State currentState = State.Visible;

        public void SetVisibilityState(State state)
        {
            currentState = state;
        }

        public State GetCurrentState()
        {
            return currentState;
        }
    }
}