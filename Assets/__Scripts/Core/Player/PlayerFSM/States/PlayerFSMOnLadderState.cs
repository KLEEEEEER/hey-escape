using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMOnLadderState : PlayerFSMBaseState
    {
        int currentLadders = 0;
        bool canControlHorizontal = false;
        bool isGroundAhead = false;


        public PlayerFSMOnLadderState(PlayerFSM playerFSM) : base(playerFSM) { }
        public override void EnterState()
        {
#if UNITY_ANDROID || UNITY_IPHONE
            OnUseButtonPressed.AddListener(useButtonPressed);
#endif
            fsm.Animator.SetBool("IsJumping", false);
            fsm.Animator.SetBool("IsClimbing", true);
            currentLadders = 1;
            canControlHorizontal = false;
        }

        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Climbable"))
            {
                currentLadders++;
            }
        }

        public override void FixedUpdate()
        {
            if (fsm.GroundAhead.Check())
            {
                if (fsm.arrow.activeSelf)
                    fsm.arrow.SetActive(false);
            } 
            else
            {
                if (!fsm.arrow.activeSelf)
                    fsm.arrow.SetActive(true);
            }
        }

        public override void Update()
        {
            fsm.Rigidbody2D.gravityScale = 0f;
            fsm.Rigidbody2D.velocity = new Vector2(0, 0);
            if (!canControlHorizontal)
            {
                fsm.PlayerMovement.MoveVertically(fsm.InputHandler.Vertical);
            }
            else
            {
                fsm.transform.position = new Vector2(fsm.transform.position.x + fsm.InputHandler.Horizontal * fsm.PlayerAttributes.ClimbingSpeedMultiplier * Time.deltaTime, fsm.transform.position.y + Mathf.Ceil(fsm.InputHandler.Vertical) * fsm.PlayerAttributes.ClimbingSpeedMultiplier * Time.deltaTime);
            }
            fsm.Animator.SetFloat("ClimbDirection", Mathf.Clamp(fsm.joystick.Vertical, -1f, 1f));
        }
        public override void OnTriggerExit2D(Collider2D collision)
        {
            fsm.Rigidbody2D.gravityScale = fsm.DefaultGravityScale;
            if (collision.CompareTag("Climbable"))
            {
                currentLadders--;
                if (currentLadders <= 0)
                {
                    OnUseButtonPressed.RemoveListener(useButtonPressed);
                    fsm.Animator.SetBool("IsClimbing", false);
                    fsm.Animator.speed = 1f;
                    fsm.arrow.SetActive(false);
                    fsm.TransitionToState(fsm.IdleState);
                }
            }
        }

        public void playerCanControlHorizontal()
        {
            canControlHorizontal = true;
        }

        public bool IsGroundAhead()
        {
            return isGroundAhead;
        }

#if UNITY_ANDROID || UNITY_IPHONE
        private void useButtonPressed()
        {
            if (!IsGroundAhead())
            {
                PlayerFSM player = GameManager.instance.PlayerFSM;
                float forceFromLadder = (player.isLookingRight) ? player.PlayerAttributes.JumpFromLadderForce : (player.PlayerAttributes.JumpFromLadderForce * -1f);
                player.Rigidbody2D.AddForce(new Vector2(forceFromLadder, 0f));
            }
        }
#endif
    }
}