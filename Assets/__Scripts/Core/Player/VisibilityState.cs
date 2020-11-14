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

        public State currentState { get; private set; }

        private void Start()
        {
            currentState = State.Visible;
        }

        public void SetVisibilityState(State state)
        {
            currentState = state;
        }
    }
}