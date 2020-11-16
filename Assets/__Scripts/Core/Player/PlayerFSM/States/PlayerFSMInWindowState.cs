using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMInWindowState : PlayerFSMBaseState
    {
        public UnityEvent OnWindowExit = new UnityEvent();
        public override void EnterState(PlayerFSM player)
        {
#if UNITY_ANDROID || UNITY_IPHONE
            OnUseButtonPressed.AddListener(useButtonPressed);
#endif
            GameManager.instance.PlayerMovement.SetEnabled(true);
        }

        public override void OnTriggerEnter2D(PlayerFSM player, Collider2D collision)
        {

        }

        public override void LateUpdate(PlayerFSM player)
        {

        }

        public override void FixedUpdate(PlayerFSM player)
        {

        }

        public override void Update(PlayerFSM player)
        {
            /*if (Input.GetKeyDown(KeyCode.E))
            {
                if (GameManager.instance.PlayerRenderer != null) GameManager.instance.PlayerRenderer.enabled = true;
                GameManager.instance.CharacterController2D.TransitionToState(GameManager.instance.CharacterController2D.IdleState);
                GameManager.instance.PlayerMovement.disableMovement = false;
                GameManager.instance.PlayerComponent.UnhidePlayer();
                OnWindowExit.Invoke();
                OnWindowExit.RemoveAllListeners();
            }*/
        }
        public override void OnTriggerExit2D(PlayerFSM player, Collider2D collision)
        {

        }

#if UNITY_ANDROID || UNITY_IPHONE
        private void useButtonPressed()
        {
            if (GameManager.instance.PlayerRenderer != null) GameManager.instance.PlayerRenderer.enabled = true;
            GameManager.instance.PlayerMovement.SetEnabled(false);
            GameManager.instance.PlayerComponent.UnhidePlayer();
            OnWindowExit.Invoke();
            OnWindowExit.RemoveAllListeners();
            OnUseButtonPressed.RemoveListener(useButtonPressed);
            GameManager.instance.PlayerFSM.TransitionToState(GameManager.instance.PlayerFSM.IdleState);
        }
#endif
    }
}