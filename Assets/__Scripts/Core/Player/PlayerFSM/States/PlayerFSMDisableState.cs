using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMDisableState : PlayerFSMBaseState
    {
        public override void EnterState(PlayerFSM player)
        {
            player.Animator.SetBool("IsRunning", false);
            player.Animator.SetTrigger("Hide");
            player.Rigidbody2D.velocity = Vector2.zero;
            player.Rigidbody2D.isKinematic = true;
        }

        public override void ExitState(PlayerFSM player)
        {
            player.Rigidbody2D.isKinematic = false;
        }

        public override void FixedUpdate(PlayerFSM player)
        {

        }

        public override void LateUpdate(PlayerFSM player)
        {

        }

        public override void OnTriggerEnter2D(PlayerFSM player, Collider2D collision)
        {

        }

        public override void OnTriggerExit2D(PlayerFSM player, Collider2D collision)
        {

        }

        public override void Update(PlayerFSM player)
        {

        }
    }
}