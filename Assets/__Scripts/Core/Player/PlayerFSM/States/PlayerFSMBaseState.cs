using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMBaseState
    {
        public static UnityEvent OnUseButtonPressed = new UnityEvent();
        public static UnityEvent OnJumpButtonPressed = new UnityEvent();
        public virtual void EnterState(PlayerFSM player) { }
        public virtual void ExitState(PlayerFSM player) { }
        public virtual void Update(PlayerFSM player) { }
        public virtual void OnTriggerEnter2D(PlayerFSM player, Collider2D collision) { }
        public virtual void OnTriggerExit2D(PlayerFSM player, Collider2D collision) { }
        public virtual void FixedUpdate(PlayerFSM player) { }
        public virtual void LateUpdate(PlayerFSM player) { }
    }
}