﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMHiddenState : PlayerFSMBaseState
    {
        public PlayerFSMHiddenState(PlayerFSM playerFSM) : base(playerFSM) { }
        public override void EnterState()
        {
            fsm.Animator.SetBool("IsRunning", false);
            fsm.Animator.SetTrigger("Hide");
            fsm.Rigidbody2D.velocity = Vector2.zero;
            fsm.Rigidbody2D.isKinematic = true;
        }

        public override void ExitState()
        {
            fsm.Rigidbody2D.isKinematic = false;
        }

        public override void Update()
        {
            if (fsm.InputHandler.Vertical < 0.8f && fsm.DetectorHandler.IsHidden())
            {
                fsm.DetectorHandler.UnhideFromHidePlace();
                fsm.TransitionToState(fsm.IdleState);
            }
        }
    }
}