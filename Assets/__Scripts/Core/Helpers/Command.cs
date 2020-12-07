using HeyEscape.Core.Player.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Helpers
{
    public class Command
    {
        public virtual void Execute(PlayerFSM player) { }
    }
}