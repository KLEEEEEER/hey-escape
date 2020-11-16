using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMIdleState : PlayerFSMBaseState
    {
        public override void EnterState(PlayerFSM player)
        {
            player.Animator.SetBool("IsRunning", false);
            player.Animator.SetBool("IsGrounded", true);
        }

        public override void FixedUpdate(PlayerFSM player)
        {

        }

        public override void LateUpdate(PlayerFSM player)
        {

        }

        public override void OnTriggerEnter2D(PlayerFSM player, Collider2D collision)
        {
            if (collision.CompareTag("Climbable"))
            {
                player.transform.position = new Vector2(collision.transform.position.x, player.transform.position.y);
                player.TransitionToState(player.LadderState);
            }
        }
        public override void OnTriggerExit2D(PlayerFSM player, Collider2D collision)
        {

        }

        public override void Update(PlayerFSM player)
        {
            if (GameManager.instance.IsGameOver) return;

#if UNITY_ANDROID || UNITY_IPHONE
            if (player.IsMobileJumpPressed)
#else
        if (Input.GetKeyDown(KeyCode.Space))
#endif
            {
                player.Rigidbody2D.AddForce(new Vector2(0f, player.JumpForce));
                player.TransitionToState(player.JumpingState);
                return;
            }

            if (player.Horizontal != 0)
            {
                player.TransitionToState(player.RunningState);
                return;
            }

            player.Animator.SetFloat("Speed", Mathf.Abs(player.Horizontal));
            player.Rigidbody2D.velocity = new Vector2(0, player.Rigidbody2D.velocity.y);
        }
    }
}