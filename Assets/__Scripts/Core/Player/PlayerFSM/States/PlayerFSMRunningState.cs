using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Player.FSM.States
{
    public class PlayerFSMRunningState : PlayerFSMBaseState
    {
        public override void EnterState(PlayerFSM player)
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

        public override void LateUpdate(PlayerFSM player)
        {

        }

        public override void FixedUpdate(PlayerFSM player)
        {
            if (player.Horizontal == 0)
            {
                player.TransitionToState(player.IdleState);
                return;
            }

            player.Animator.SetFloat("Speed", Mathf.Abs(player.Horizontal));

            player.Animator.SetBool("IsRunning", (Mathf.Abs(player.Rigidbody2D.velocity.x) > 0));

            Vector2 targetVelocity = new Vector2(player.Horizontal, player.Rigidbody2D.velocity.y);
            //player.Rigidbody2D.velocity = Vector2.SmoothDamp(player.Rigidbody2D.velocity, targetVelocity, ref player.currentVelocity, player.MovementSmoothing);
            player.Rigidbody2D.velocity = targetVelocity;
        }

        public override void Update(PlayerFSM player)
        {
#if UNITY_ANDROID || UNITY_IPHONE
            if (player.IsMobileJumpPressed)
#else
        if (Input.GetKeyDown(KeyCode.Space))
#endif
            {
                player.Rigidbody2D.AddForce(new Vector2(0f, player.JumpForce));
                player.TransitionToState(player.JumpingState);
            }
        }
        public override void OnTriggerExit2D(PlayerFSM player, Collider2D collision)
        {

        }
    }
}