using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerRunningState : CharacterControllerBaseState
{
    public override void EnterState(CharacterController2D player)
    {

    }

    public override void OnTriggerEnter2D(CharacterController2D player, Collider2D collision)
    {
        if (collision.CompareTag("Climbable"))
        {
            player.TransitionToState(player.LadderState);
        }
    }

    public override void LateUpdate(CharacterController2D player)
    {

    }

    public override void FixedUpdate(CharacterController2D player)
    {
        if (player.Horizontal == 0)
        {
            player.TransitionToState(player.IdleState);
            return;
        }

        player.Animator.SetFloat("Speed", Mathf.Abs(player.Horizontal));

        player.Animator.SetBool("IsRunning", (Mathf.Abs(player.Rigidbody2D.velocity.x) > 0));

        Vector2 targetVelocity = new Vector2(player.Horizontal, player.Rigidbody2D.velocity.y);
        player.Rigidbody2D.velocity = Vector2.SmoothDamp(player.Rigidbody2D.velocity, targetVelocity, ref player.currentVelocity, player.MovementSmoothing);
    }

    public override void Update(CharacterController2D player)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Rigidbody2D.AddForce(new Vector2(0f, player.JumpForce));
            player.TransitionToState(player.JumpingState);
        }
    }
    public override void OnTriggerExit2D(CharacterController2D player, Collider2D collision)
    {
        
    }
}
