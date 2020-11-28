using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMIdleState : PlayerFSMBaseState
    {
        public PlayerFSMIdleState(PlayerFSM playerFSM) : base(playerFSM) { }
        public override void EnterState()
        {
            fsm.Animator.SetBool("IsRunning", false);
            fsm.Animator.SetBool("IsGrounded", true);
            fsm.InputHandler.KillButtonPressed.AddListener(OnKillButtonPressed);
            fsm.InputHandler.UsingButtonPressed.AddListener(OnUsingButtonPressed);
            fsm.InputHandler.JumpButtonPressed.AddListener(OnJumpButtonPressed);
        }

        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Climbable"))
            {
                fsm.transform.position = new Vector2(collision.transform.position.x, fsm.transform.position.y);
                fsm.TransitionToState(fsm.LadderState);
            }
        }

        public override void Update()
        {
            if (GameManager.instance.IsGameOver) return;

            if (fsm.InputHandler.Vertical > 0.8f && !fsm.DetectorHandler.IsHidden())
            {
                if (fsm.DetectorHandler.TryHideInHidePlace(() =>
                    {
                        fsm.Animator.SetBool("IsJumping", false);
                        fsm.Animator.SetBool("IsRunning", false);
                        fsm.Animator.SetBool("IsGrounded", true);
                        //player.TransitionToState(player.HiddenState);
                    }))
                {
                    fsm.TransitionToState(fsm.HiddenState);
                    return;
                }
            }

            if (fsm.InputHandler.Horizontal != 0)
            {
                fsm.TransitionToState(fsm.RunningState);
                return;
            }

            fsm.Animator.SetFloat("Speed", Mathf.Abs(fsm.InputHandler.Horizontal));
            fsm.Rigidbody2D.velocity = new Vector2(0, fsm.Rigidbody2D.velocity.y);
        }

        private void OnKillButtonPressed()
        {
            fsm.DetectorHandler.InteractKillable(() => { fsm.Animator.SetTrigger("Kill"); });
        }

        private void OnUsingButtonPressed()
        {
            fsm.DetectorHandler.InteractInteractable();
            fsm.DetectorHandler.InteractSearchable();
        }

        private void OnJumpButtonPressed()
        {
            fsm.Rigidbody2D.AddForce(new Vector2(0f, fsm.PlayerAttributes.JumpForce));
            fsm.TransitionToState(fsm.JumpingState);
        }

        public override void ExitState()
        {
            fsm.InputHandler.KillButtonPressed.RemoveListener(OnKillButtonPressed);
            fsm.InputHandler.UsingButtonPressed.RemoveListener(OnUsingButtonPressed);
            fsm.InputHandler.JumpButtonPressed.RemoveListener(OnJumpButtonPressed);
        }
    }
}