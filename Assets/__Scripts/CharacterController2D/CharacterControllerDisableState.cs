using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerDisableState : CharacterControllerBaseState
{
    public override void EnterState(CharacterController2D player)
    {
        player.Animator.SetBool("IsRunning", false);
        player.Animator.SetTrigger("Hide");
    }

    public override void FixedUpdate(CharacterController2D player)
    {

    }

    public override void LateUpdate(CharacterController2D player)
    {

    }

    public override void OnCollisionEnter(CharacterController2D player)
    {
        
    }

    public override void Update(CharacterController2D player)
    {
        
    }
}
