using HeyEscape.Core.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.Commands
{
    public class KillCommand : Command
    {
        public override void Execute(PlayerFSM player)
        {
            player.DetectorHandler.InteractKillable(() => { player.Animator.SetTrigger("Kill"); });
        }
    }
}