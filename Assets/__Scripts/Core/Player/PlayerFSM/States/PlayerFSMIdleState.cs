﻿using HeyEscape.Core.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMIdleState : PlayerFSMBaseState
    {
        public PlayerFSMIdleState(PlayerFSM playerFSM) : base(playerFSM) { }
        public override void EnterState()
        {
            fsm.Animator.SetBool("IsRunning", false);
            fsm.Animator.SetBool("IsGrounded", true);
            fsm.InputHandler.KillButtonPressed.AddListener(OnKillButtonPressed);
            fsm.InputHandler.UsingButtonPressed.AddListener(OnUsingButtonPressed);
            fsm.InputHandler.JumpButtonPressed.AddListener(OnJumpButtonPressed);
            //fsm.InputHandler.ScreenTouched.AddListener(OnScreenTouched);
        }

        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Climbable"))
            {
                fsm.transform.position = new Vector2(collision.transform.position.x, fsm.transform.position.y);
                fsm.TransitionToState(fsm.LadderState);
            }
        }

        public override void Update()
        {
            if (GameStateChanger.Instance.CompareState(GameStateChanger.GameState.GameOver)) return;

            if (fsm.InputHandler.Horizontal != 0)
            {
                fsm.TransitionToState(fsm.RunningState);
                return;
            }

            if (fsm.InputHandler.Vertical >= 0.8f && !fsm.DetectorHandler.IsHidden())
            {
                fsm.HideCommand.Execute(fsm);
                return;
            }

            fsm.Animator.SetFloat("Speed", Mathf.Abs(fsm.InputHandler.Horizontal));
            fsm.Rigidbody2D.velocity = new Vector2(0, fsm.Rigidbody2D.velocity.y);
        }

        private void OnKillButtonPressed()
        {
            fsm.KillCommand.Execute(fsm);
        }

        private void OnUsingButtonPressed()
        {
            fsm.UseCommand.Execute(fsm);
        }

        private void OnJumpButtonPressed()
        {
            fsm.JumpCommand.Execute(fsm);
        }

        public override void ExitState()
        {
            fsm.InputHandler.KillButtonPressed.RemoveListener(OnKillButtonPressed);
            fsm.InputHandler.UsingButtonPressed.RemoveListener(OnUsingButtonPressed);
            fsm.InputHandler.JumpButtonPressed.RemoveListener(OnJumpButtonPressed);
            //fsm.InputHandler.ScreenTouched.RemoveListener(OnScreenTouched);
        }

        private void OnScreenTouched(RaycastHit2D hit)
        {
            
        }
    }
}