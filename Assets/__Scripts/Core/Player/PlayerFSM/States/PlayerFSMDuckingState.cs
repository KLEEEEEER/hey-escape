using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMDuckingState : PlayerFSMBaseState
    {
        public override void EnterState(PlayerFSM player)
        {

        }

        public override void FixedUpdate(PlayerFSM player)
        {

        }

        public override void LateUpdate(PlayerFSM player)
        {

        }

        public override void OnTriggerEnter2D(PlayerFSM player, Collider2D collision)
        {
            if (collision.CompareTag("Climbable"))
            {
                player.TransitionToState(player.LadderState);
            }
        }

        public override void Update(PlayerFSM player)
        {

        }
        public override void OnTriggerExit2D(PlayerFSM player, Collider2D collision)
        {

        }
    }
}