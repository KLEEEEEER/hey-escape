using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerOnLadderState : CharacterControllerBaseState
{

    public override void EnterState(CharacterController2D player)
    {
        player.Animator.SetBool("IsJumping", false);
    }

    public override void OnTriggerEnter2D(CharacterController2D player, Collider2D collision)
    {

    }

    public override void LateUpdate(CharacterController2D player)
    {

    }

    public override void FixedUpdate(CharacterController2D player)
    {
        
    }

    public override void Update(CharacterController2D player)
    {
        player.Rigidbody2D.gravityScale = 0f;
        player.Rigidbody2D.velocity = new Vector2(0, 0);
        player.transform.position = new Vector2(player.transform.position.x + player.Horizontal * player.ClimbingSpeedMultiplier * Time.deltaTime, player.transform.position.y + player.Vertical * player.ClimbingSpeedMultiplier * Time.deltaTime);
    }
    public override void OnTriggerExit2D(CharacterController2D player, Collider2D collision)
    {
        player.Rigidbody2D.gravityScale = player.DefaultGravityScale;
        if (collision.CompareTag("Climbable"))
        {
            player.TransitionToState(player.IdleState);
        }
    }
}
