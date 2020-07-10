using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerOnLadderState : CharacterControllerBaseState
{
    int currentLadders = 0;
    bool canControlHorizontal = false;
    bool isGroundAhead = false;


    public override void EnterState(CharacterController2D player)
    {
        player.Animator.SetBool("IsJumping", false);
        currentLadders = 1;
        canControlHorizontal = false;
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
        RaycastHit2D[] hits =  Physics2D.RaycastAll(
            player.CheckForGroundFeet.position, 
            (player.CheckForGroundHead.position - player.CheckForGroundFeet.position).normalized, 
            Vector2.Distance(player.CheckForGroundFeet.position, player.CheckForGroundHead.position), 
            player.GroundLayers
        );
        isGroundAhead = (hits.Length > 0);
        Debug.Log("isGroundAhead = " + isGroundAhead);
    }

    public override void Update(CharacterController2D player)
    {
        player.Rigidbody2D.gravityScale = 0f;
        player.Rigidbody2D.velocity = new Vector2(0, 0);
        if (!canControlHorizontal)
        {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + Mathf.Ceil(player.Vertical) * player.ClimbingSpeedMultiplier * Time.deltaTime);
        }
        else
        {
            player.transform.position = new Vector2(player.transform.position.x + player.Horizontal * player.ClimbingSpeedMultiplier * Time.deltaTime, player.transform.position.y + Mathf.Ceil(player.Vertical) * player.ClimbingSpeedMultiplier * Time.deltaTime);
        }
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

    public void playerCanControlHorizontal()
    {
        canControlHorizontal = true;
    }

    public bool IsGroundAhead()
    {
        return isGroundAhead;
    }
}
