using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMJumpingState : PlayerFSMBaseState
    {
        Collider2D[] colliders;
        private float timer = 0f;
        private float delayGroundCheck = 0.5f;
        public PlayerFSMJumpingState(PlayerFSM playerFSM) : base(playerFSM) { }
        public override void EnterState()
        {
            Debug.Log("Jumping wat?");
            fsm.OnJumpEvent.Invoke();
            fsm.Animator.SetBool("IsJumping", true);
            timer = 0f;
        }

        public override void FixedUpdate()
        {
            if (fsm.airControl)
            {
                Vector2 targetVelocity = new Vector2(fsm.InputHandler.Horizontal, fsm.Rigidbody2D.velocity.y);
                fsm.Rigidbody2D.velocity = Vector2.SmoothDamp(fsm.Rigidbody2D.velocity, targetVelocity, ref fsm.currentVelocity, fsm.MovementSmoothing);
            }

            if (fsm.Rigidbody2D.velocity.y < 0)
            {
                fsm.Animator.SetBool("IsJumping", false);
            }
        }

        public override void LateUpdate()
        {
            if (timer > delayGroundCheck)
            {
                if (fsm.IsGrounded)
                {
                    fsm.Animator.SetBool("IsJumping", false);
                    if (Mathf.Abs(fsm.Rigidbody2D.velocity.x) > 0)
                    {
                        fsm.TransitionToState(fsm.RunningState);
                    }
                    else
                    {
                        fsm.TransitionToState(fsm.IdleState);
                    }
                }
            }
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
            timer += Time.deltaTime;
        }
    }
}