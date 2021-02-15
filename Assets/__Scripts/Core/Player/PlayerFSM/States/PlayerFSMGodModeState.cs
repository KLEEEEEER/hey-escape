#if UNITY_EDITOR || DEVELOPMENT_BUILD
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMGodModeState : PlayerFSMBaseState
    {
        public PlayerFSMGodModeState(PlayerFSM playerFSM) : base(playerFSM) { }
        public override void EnterState()
        {
            fsm.Rigidbody2D.isKinematic = true;
            fsm.SetEnableColliders(false);
            fsm.Visibility.SetVisibilityState(VisibilityState.State.Hidden);
            fsm.Animator.SetBool("IsJumping", true);
        }

        public override void FixedUpdate()
        {
            Vector2 direction = new Vector2(fsm.InputHandler.Horizontal, fsm.InputHandler.Vertical);
            fsm.PlayerMovement.Move(direction, 2f);
        }

        public override void ExitState()
        {
            fsm.Rigidbody2D.isKinematic = false;
            fsm.SetEnableColliders(true);
            fsm.Visibility.SetVisibilityState(VisibilityState.State.Visible);
            fsm.Animator.SetBool("IsJumping", false);
        }
    }
}
#endif