using HeyEscape.Core.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.Commands
{
    public class HideCommand : Command
    {
        public override void Execute(PlayerFSM player)
        {
            if (player.DetectorHandler.TryHideInHidePlace(() =>
            {
                player.Animator.SetBool("IsJumping", false);
                player.Animator.SetBool("IsRunning", false);
                player.Animator.SetBool("IsGrounded", true);
            }))
            {
                player.TransitionToState(player.HiddenState);
                return;
            }
        }
    }
}