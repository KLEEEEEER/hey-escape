using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Player.FSM.States
{
    public abstract class PlayerFSMBaseState
    {
        public static UnityEvent OnUseButtonPressed = new UnityEvent();
        public static UnityEvent OnJumpButtonPressed = new UnityEvent();
        public abstract void EnterState(PlayerFSM player);
        public abstract void Update(PlayerFSM player);
        public abstract void OnTriggerEnter2D(PlayerFSM player, Collider2D collision);
        public abstract void OnTriggerExit2D(PlayerFSM player, Collider2D collision);
        public abstract void FixedUpdate(PlayerFSM player);
        public abstract void LateUpdate(PlayerFSM player);
    }
}