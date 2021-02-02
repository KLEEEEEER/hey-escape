using HeyEscape.Core.Helpers;

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