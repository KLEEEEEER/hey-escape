using HeyEscape.Core.Helpers;

namespace HeyEscape.Core.Player.FSM.Commands
{
    public class HideCommand : Command
    {
        public override void Execute(PlayerFSM player)
        {
            if (player.DetectorHandler.TryHideInHidePlace())
            {
                player.TransitionToState(player.HiddenState);
                return;
            }
        }
    }
}