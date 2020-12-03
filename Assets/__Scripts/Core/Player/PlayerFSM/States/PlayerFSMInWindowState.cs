﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMInWindowState : PlayerFSMBaseState
    {
        public UnityEvent<PlayerFSM> OnWindowExit = new UnityEvent<PlayerFSM>();
        public PlayerFSMInWindowState(PlayerFSM playerFSM) : base(playerFSM) { }
        public override void EnterState()
        {
            fsm.Visibility.SetVisibilityState(VisibilityState.State.Hidden);
            fsm.PlayerMovement.SetEnabled(false);
            fsm.InputHandler.UsingButtonPressed.AddListener(OnUsingButtonPressed);
        }

        private void OnUsingButtonPressed()
        {
            if (fsm.Renderer != null) fsm.Renderer.enabled = true;
            OnWindowExit.Invoke(fsm);
            OnWindowExit.RemoveAllListeners();
            fsm.TransitionToState(fsm.IdleState);
        }

        public override void ExitState()
        {
            fsm.Visibility.SetVisibilityState(VisibilityState.State.Visible);
            fsm.InputHandler.UsingButtonPressed.RemoveListener(OnUsingButtonPressed);
            fsm.PlayerMovement.SetEnabled(true);
        }
    }
}