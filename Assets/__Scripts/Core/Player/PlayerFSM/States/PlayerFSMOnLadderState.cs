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
            fsm.Animator.SetBool("IsJumping", false);
            fsm.Animator.SetBool("IsClimbing", true);
            currentLadders = 1;
            canControlHorizontal = false;
            fsm.InputHandler.UsingButtonPressed.AddListener(OnJumpFromLadderPressed);
            fsm.InputHandler.JumpButtonPressed.AddListener(OnJumpFromLadderPressed);
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
                fsm.PlayerMovement.MoveVertically(fsm.InputHandler.Vertical, fsm.InputHandler.Horizontal);
            }
            else
            {
                fsm.transform.position = new Vector2(fsm.transform.position.x + fsm.InputHandler.Horizontal * fsm.PlayerAttributes.ClimbingSpeedMultiplier * Time.deltaTime, fsm.transform.position.y + Mathf.Ceil(fsm.InputHandler.Vertical) * fsm.PlayerAttributes.ClimbingSpeedMultiplier * Time.deltaTime);
            }
            fsm.Animator.SetFloat("ClimbDirection", Mathf.Clamp(fsm.InputHandler.Vertical, -1f, 1f));
        }

        public override void ExitState()
        {
            fsm.Animator.SetFloat("ClimbDirection", 0f);
            fsm.InputHandler.UsingButtonPressed.RemoveListener(OnJumpFromLadderPressed);
            fsm.InputHandler.JumpButtonPressed.RemoveListener(OnJumpFromLadderPressed);
        }

        public override void OnTriggerExit2D(Collider2D collision)
        {
            fsm.Rigidbody2D.gravityScale = fsm.DefaultGravityScale;
            if (collision.CompareTag("Climbable"))
            {
                currentLadders--;
                if (currentLadders <= 0)
                {
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
        private void OnJumpFromLadderPressed()
        {
            if (!IsGroundAhead())
            {
                float forceFromLadder = (fsm.PlayerMovement.IsLookingRight) ? fsm.PlayerAttributes.JumpFromLadderForce : (fsm.PlayerAttributes.JumpFromLadderForce * -1f);
                fsm.Rigidbody2D.AddForce(new Vector2(forceFromLadder, 0f));
            }
        }
#endif
    }
}