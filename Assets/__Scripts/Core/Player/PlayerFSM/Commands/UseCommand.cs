using HeyEscape.Core.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.Commands
{
    public class UseCommand : Command
    {
        public override void Execute(PlayerFSM player)
        {
            player.DetectorHandler.InteractInteractable();
            player.DetectorHandler.InteractSearchable(() => {
                player.DisableForTime(0.7f);
                player.Animator.SetTrigger("Search"); 
            });
        }
    }
}