using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMHiddenState : PlayerFSMBaseState
    {
        public PlayerFSMHiddenState(PlayerFSM playerFSM) : base(playerFSM) { }
        public override void EnterState()
        {
            Debug.Log("Enter Hidden state");
            fsm.Animator.SetBool("IsRunning", false);
            fsm.Animator.SetTrigger("Hide");
            fsm.Rigidbody2D.velocity = Vector2.zero;
            fsm.Rigidbody2D.isKinematic = true;
            fsm.SetEnableColliders(false);
        }

        public override void ExitState()
        {
            Debug.Log("Exit Hidden state");
            fsm.Animator.SetTrigger("Unhide");
            fsm.Rigidbody2D.isKinematic = false;
            fsm.SetEnableColliders(true);
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