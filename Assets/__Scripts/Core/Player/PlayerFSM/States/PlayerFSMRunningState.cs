using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMRunningState : PlayerFSMBaseState
    {
        public PlayerFSMRunningState(PlayerFSM playerFSM) : base(playerFSM) { }

        public override void EnterState()
        {
            Debug.Log("Running state");
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

        public override void FixedUpdate()
        {
            if (fsm.InputHandler.Horizontal == 0)
            {
                fsm.TransitionToState(fsm.IdleState);
                return;
            }

            fsm.Animator.SetFloat("Speed", Mathf.Abs(fsm.InputHandler.Horizontal));

            fsm.Animator.SetBool("IsRunning", (Mathf.Abs(fsm.Rigidbody2D.velocity.x) > 0));

            /*Vector2 targetVelocity = new Vector2(player.InputHandler.Horizontal, player.Rigidbody2D.velocity.y);
            //player.Rigidbody2D.velocity = Vector2.SmoothDamp(player.Rigidbody2D.velocity, targetVelocity, ref player.currentVelocity, player.MovementSmoothing);
            player.Rigidbody2D.velocity = targetVelocity;*/
            fsm.PlayerMovement.MoveHorizontally(fsm.InputHandler.Horizontal);
        }

        public override void Update()
        {
#if UNITY_ANDROID || UNITY_IPHONE
            if (fsm.IsMobileJumpPressed)
#else
        if (Input.GetKeyDown(KeyCode.Space))
#endif
            {
                fsm.Rigidbody2D.AddForce(new Vector2(0f, fsm.PlayerAttributes.JumpForce));
                fsm.TransitionToState(fsm.JumpingState);
                return;
            }

            if (fsm.InputHandler.Vertical > 0.7f && !fsm.DetectorHandler.IsHidden())
            {
                if (fsm.DetectorHandler.TryHideInHidePlace(() =>
                {
                    fsm.Animator.SetBool("IsJumping", false);
                    fsm.Animator.SetBool("IsRunning", false);
                    fsm.Animator.SetBool("IsGrounded", true);
                }))
                {
                    fsm.TransitionToState(fsm.HiddenState);
                    return;
                }
            }
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