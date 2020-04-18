using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerIdleState : CharacterControllerBaseState
{
    public override void EnterState(CharacterController2D player)
    {
        player.Animator.SetBool("IsRunning", false);
        player.Animator.SetBool("IsGrounded", true);
    }

    public override void FixedUpdate(CharacterController2D player)
    {

    }

    public override void LateUpdate(CharacterController2D player)
    {

    }

    public override void OnTriggerEnter2D(CharacterController2D player, Collider2D collision)
    {
        if (collision.CompareTag("Climbable"))
        {
            player.TransitionToState(player.LadderState);
        }
    }
    public override void OnTriggerExit2D(CharacterController2D player, Collider2D collision)
    {

    }

    public override void Update(CharacterController2D player)
    {
        if (GameManager.instance.IsGameOver) return;

        if (Input.GetKeyDown(KeyCode.Space))
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
