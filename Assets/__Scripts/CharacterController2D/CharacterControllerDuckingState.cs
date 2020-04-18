using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerDuckingState : CharacterControllerBaseState
{
    public override void EnterState(CharacterController2D player)
    {
        
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

    public override void Update(CharacterController2D player)
    {

    }
    public override void OnTriggerExit2D(CharacterController2D player, Collider2D collision)
    {

    }
}
