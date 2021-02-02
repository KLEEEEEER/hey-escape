using UnityEngine;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMDisableState : PlayerFSMBaseState
    {
        public PlayerFSMDisableState(PlayerFSM playerFSM) : base(playerFSM) { }
        public override void EnterState()
        {
            fsm.Animator.SetBool("IsRunning", false);
            //fsm.Animator.SetTrigger("Hide");
            fsm.Rigidbody2D.velocity = Vector2.zero;
            fsm.Rigidbody2D.isKinematic = true;
            fsm.SetEnableColliders(false);
        }

        public override void ExitState()
        {
            //fsm.Animator.SetTrigger("Unhide");
            fsm.Rigidbody2D.isKinematic = false;
            fsm.SetEnableColliders(true);
        }
    }
}