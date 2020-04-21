using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerOnLadderState : CharacterControllerBaseState
{
    int currentLadders = 0;


    public override void EnterState(CharacterController2D player)
    {
        player.Animator.SetBool("IsJumping", false);
        currentLadders = 1;
    }

    public override void OnTriggerEnter2D(CharacterController2D player, Collider2D collision)
    {
        if (collision.CompareTag("Climbable"))
        {
            currentLadders++;
        }
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
            currentLadders--;
            if (currentLadders <= 0)
                player.TransitionToState(player.IdleState);
        }
    }
}
