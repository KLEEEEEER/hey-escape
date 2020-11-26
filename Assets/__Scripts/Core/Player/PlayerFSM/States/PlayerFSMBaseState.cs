﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMBaseState
    {
        protected PlayerFSM fsm;

        public static UnityEvent OnUseButtonPressed = new UnityEvent();
        public static UnityEvent OnJumpButtonPressed = new UnityEvent();

        public PlayerFSMBaseState(PlayerFSM playerFSM) {
            fsm = playerFSM;
        }
        public virtual void EnterState() { }
        public virtual void ExitState() { }
        public virtual void Update() { }
        public virtual void OnTriggerEnter2D(Collider2D collision) { }
        public virtual void OnTriggerExit2D(Collider2D collision) { }
        public virtual void FixedUpdate() { }
        public virtual void LateUpdate() { }
    }
}