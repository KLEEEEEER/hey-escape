using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMInWindowState : PlayerFSMBaseState
    {
        public UnityEvent OnWindowExit = new UnityEvent();
        public PlayerFSMInWindowState(PlayerFSM playerFSM) : base(playerFSM) { }
        public override void EnterState()
        {
#if UNITY_ANDROID || UNITY_IPHONE
            OnUseButtonPressed.AddListener(useButtonPressed);
#endif
            GameManager.instance.PlayerMovement.SetEnabled(true);
        }

#if UNITY_ANDROID || UNITY_IPHONE
        private void useButtonPressed()
        {
            if (GameManager.instance.PlayerRenderer != null) GameManager.instance.PlayerRenderer.enabled = true;
            GameManager.instance.PlayerMovement.SetEnabled(false);
            OnWindowExit.Invoke();
            OnWindowExit.RemoveAllListeners();
            OnUseButtonPressed.RemoveListener(useButtonPressed);
            GameManager.instance.PlayerFSM.TransitionToState(GameManager.instance.PlayerFSM.IdleState);
        }
#endif
    }
}