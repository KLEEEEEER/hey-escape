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
        public override void EnterState(PlayerFSM player)
        {
            Debug.Log("Jumping wat?");
            player.OnJumpEvent.Invoke();
            player.Animator.SetBool("IsJumping", true);
            timer = 0f;
        }

        public override void FixedUpdate(PlayerFSM player)
        {
            if (player.airControl)
            {
                Vector2 targetVelocity = new Vector2(player.InputHandler.Horizontal, player.Rigidbody2D.velocity.y);
                player.Rigidbody2D.velocity = Vector2.SmoothDamp(player.Rigidbody2D.velocity, targetVelocity, ref player.currentVelocity, player.MovementSmoothing);
            }

            if (player.Rigidbody2D.velocity.y < 0)
            {
                player.Animator.SetBool("IsJumping", false);
            }
        }

        public override void LateUpdate(PlayerFSM player)
        {
            if (timer > delayGroundCheck)
            {
                if (player.IsGrounded)
                {
                    player.Animator.SetBool("IsJumping", false);
                    if (Mathf.Abs(player.Rigidbody2D.velocity.x) > 0)
                    {
                        player.TransitionToState(player.RunningState);
                    }
                    else
                    {
                        player.TransitionToState(player.IdleState);
                    }
                }
            }
        }

        public override void OnTriggerEnter2D(PlayerFSM player, Collider2D collision)
        {
            if (collision.CompareTag("Climbable"))
            {
                player.transform.position = new Vector2(collision.transform.position.x, player.transform.position.y);
                player.TransitionToState(player.LadderState);
            }
        }

        public override void Update(PlayerFSM player)
        {
            //player.Animator.SetFloat("Speed", 0);

            // player.Animator.SetFloat("VerticalSpeed", Mathf.Abs(player.Rigidbody2D.velocity.y));

            /*colliders = Physics2D.OverlapCircleAll(player.GroundCheck.position, player.GroundedRadius, player.GroundLayers);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != player.gameObject)
                {
                    player.OnLandEvent.Invoke();

                    player.Animator.SetBool("IsJumping", false);
                    player.Animator.SetBool("IsGrounded", true);

                    if (player.Rigidbody2D.velocity.x > 0)
                    {
                        player.TransitionToState(player.RunningState);
                    }
                    else
                    {
                        player.TransitionToState(player.IdleState);
                    }
                    break;
                }
            }*/

            timer += Time.deltaTime;
        }
        public override void OnTriggerExit2D(PlayerFSM player, Collider2D collision)
        {

        }
    }
}