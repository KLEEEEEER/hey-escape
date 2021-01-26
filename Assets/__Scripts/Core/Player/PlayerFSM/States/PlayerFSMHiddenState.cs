using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMHiddenState : PlayerFSMBaseState
    {
        private bool currentlyExiting = false;

        public PlayerFSMHiddenState(PlayerFSM playerFSM) : base(playerFSM) { }
        public override void EnterState()
        {
            currentlyExiting = false;
            fsm.Animator.SetBool("IsRunning", false);
            fsm.Animator.ResetTrigger("Unhide");
            fsm.Animator.SetTrigger("Hide");
            fsm.Rigidbody2D.velocity = Vector2.zero;
            fsm.Rigidbody2D.isKinematic = true;
            fsm.SetEnableColliders(false);
        }

        public override void ExitState()
        {
            fsm.Animator.SetTrigger("Unhide");
            fsm.Rigidbody2D.isKinematic = false;
            fsm.SetEnableColliders(true);
        }

        public override void Update()
        {
            if (currentlyExiting) return;

            if (fsm.InputHandler.Vertical < 0.8f && fsm.DetectorHandler.IsHidden() && fsm.CurrentState == fsm.HiddenState)
            {
                currentlyExiting = true;
                fsm.DetectorHandler.UnhideFromHidePlace();
                fsm.TransitionToState(fsm.IdleState);
            }
        }
    }
}