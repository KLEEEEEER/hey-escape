using HeyEscape.Core.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.Commands
{
    public class JumpCommand : Command
    {
        public override void Execute(PlayerFSM player)
        {
            player.Rigidbody2D.AddForce(new Vector2(0f, player.PlayerAttributes.JumpForce));
            player.TransitionToState(player.JumpingState);
        }
    }
}