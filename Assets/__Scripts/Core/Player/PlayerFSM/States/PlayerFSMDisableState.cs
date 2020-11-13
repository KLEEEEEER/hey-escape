using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Player.FSM.States
{
    public class PlayerFSMDisableState : PlayerFSMBaseState
    {
        public override void EnterState(PlayerFSM player)
        {
            player.Animator.SetBool("IsRunning", false);
            player.Animator.SetTrigger("Hide");
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