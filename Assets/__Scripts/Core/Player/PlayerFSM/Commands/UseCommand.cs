﻿using HeyEscape.Core.Helpers;

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