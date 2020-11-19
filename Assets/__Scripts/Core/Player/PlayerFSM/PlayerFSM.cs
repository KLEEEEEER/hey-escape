using HeyEscape.Core.Player.FSM.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HeyEscape.Core.Player.FSM
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerFSM : MonoBehaviour
    {
        [SerializeField] private InputHandler inputHandler;
        public InputHandler InputHandler { get => inputHandler; set { inputHandler = value; } }
        [SerializeField] private PlayerMovement playerMovement;
        public PlayerMovement PlayerMovement { get => playerMovement; set { playerMovement = value; } }

        [Header("Character Attributes")]
        public float speed;
        public float ClimbingSpeedMultiplier;
        public Vector2 currentVelocity;
        public float MovementSmoothing;
        public float JumpForce;
        public bool airControl = true;
        public Transform GroundCheck;
        public float GroundedRadius = .2f;
        public LayerMask GroundLayers;
        public Transform CeilingCheck;
        public float CeilingRadius = .25f;
        [HideInInspector] public bool IsGrounded = true;
        public float HorizontalMove = 10f;
        public float VerticalMove = 5f;
        [HideInInspector] public float DefaultGravityScale;
        public Joystick joystick;
        public float climbingSpeed = 10;
        public bool IsMobileJumpPressed = false;
        public float jumpFromLadderForce = 1000f;

        public Transform CheckForGroundHead;
        public Transform CheckForGroundFeet;


        [Header("Components")]
        private Rigidbody2D rigidbody2D;
        public Rigidbody2D Rigidbody2D { get => rigidbody2D; }
        public Animator Animator;

        [Header("Events")]
        public UnityEvent OnJumpEvent;
        public UnityEvent OnLandEvent;
        [System.Serializable] public class BoolEvent : UnityEvent<bool> { }
        [SerializeField] private BoolEvent OnCrouchEvent;
        Collider2D[] colliders;

        private PlayerFSMBaseState currentState;

        public PlayerFSMBaseState CurrentState
        {
            get => currentState;
        }

        public PlayerFSMIdleState IdleState = new PlayerFSMIdleState();
        public PlayerFSMJumpingState JumpingState = new PlayerFSMJumpingState();
        public PlayerFSMRunningState RunningState = new PlayerFSMRunningState();
        public PlayerFSMDuckingState DuckingState = new PlayerFSMDuckingState();
        public PlayerFSMDisableState DisableState = new PlayerFSMDisableState();
        public PlayerFSMOnLadderState LadderState = new PlayerFSMOnLadderState();
        public PlayerFSMInWindowState InWindowState = new PlayerFSMInWindowState();

        public bool isLookingRight = true;

        [SerializeField] public GameObject arrow;

        public void TransitionToState(PlayerFSMBaseState state)
        {
            currentState.ExitState(this);
            currentState = state;
            currentState.EnterState(this);
        }

        private void Awake()
        {
            if (OnLandEvent == null)
                OnLandEvent = new UnityEvent();

            if (OnJumpEvent == null)
                OnJumpEvent = new UnityEvent();

            if (OnCrouchEvent == null)
                OnCrouchEvent = new BoolEvent();
        }

        private void Start()
        {
            currentState = DisableState;

            rigidbody2D = GetComponent<Rigidbody2D>();

            DefaultGravityScale = rigidbody2D.gravityScale;

            TransitionToState(IdleState);
        }

        void Update()
        {
            CheckCharacterRotation();

            currentState.Update(this);
        }

        private void FixedUpdate()
        {
            CheckCharacterGrounded();
            currentState.FixedUpdate(this);
        }

        private void LateUpdate()
        {
            currentState.LateUpdate(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            currentState.OnTriggerEnter2D(this, collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            currentState.OnTriggerExit2D(this, collision);
        }

        void CheckCharacterRotation()
        {
            if (isLookingRight && InputHandler.Horizontal < 0)
            {
                Flip();
                isLookingRight = false;
            }
            else if (!isLookingRight && InputHandler.Horizontal > 0)
            {
                Flip();
                isLookingRight = true;
            }
        }

        void CheckCharacterGrounded()
        {
            colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, GroundLayers);
            bool foundGround = false;
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    OnLandEvent.Invoke();
                    foundGround = true;
                    break;
                }
            }
            Animator.SetBool("IsGrounded", foundGround);
            IsGrounded = foundGround;
        }

        void Flip()
        {
            transform.Rotate(0f, 180f, 0f);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(CeilingCheck.position, CeilingRadius);
            Gizmos.DrawWireSphere(GroundCheck.position, GroundedRadius);
            Gizmos.DrawLine(CheckForGroundFeet.position, CheckForGroundHead.position);
        }

        public void OnGameOver()
        {
            TransitionToState(DisableState);
        }

        public void OnMobileJumpButtonHold(BaseEventData data)
        {
            IsMobileJumpPressed = true;
        }
        public void OnMobileJumpButtonRelease(BaseEventData data)
        {
            IsMobileJumpPressed = false;
        }
        public void OnMobileUseButtonClicked(BaseEventData data)
        {
            PlayerFSMBaseState.OnUseButtonPressed.Invoke();
        }
    }
}