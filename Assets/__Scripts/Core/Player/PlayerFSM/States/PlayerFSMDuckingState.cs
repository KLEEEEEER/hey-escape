using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMDuckingState : PlayerFSMBaseState
    {
        public PlayerFSMDuckingState(PlayerFSM playerFSM) : base(playerFSM) { }

        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Climbable"))
            {
                fsm.TransitionToState(fsm.LadderState);
            }
        }
    }
}