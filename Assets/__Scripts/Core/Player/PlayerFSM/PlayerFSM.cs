﻿using Core.Player.FSM.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Core.Player.FSM
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerFSM : MonoBehaviour
    {
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

        public readonly PlayerFSMIdleState IdleState = new PlayerFSMIdleState();
        public readonly PlayerFSMJumpingState JumpingState = new PlayerFSMJumpingState();
        public readonly PlayerFSMRunningState RunningState = new PlayerFSMRunningState();
        public readonly PlayerFSMDuckingState DuckingState = new PlayerFSMDuckingState();
        public readonly PlayerFSMDisableState DisableState = new PlayerFSMDisableState();
        public readonly PlayerFSMOnLadderState LadderState = new PlayerFSMOnLadderState();
        public readonly PlayerFSMInWindowState InWindowState = new PlayerFSMInWindowState();

        private float horizontal;
        private float vertical;
        public float Horizontal { get => horizontal; }
        public float Vertical { get => vertical; }
        public bool isLookingRight = true;

        [SerializeField] public GameObject arrow;

        public void TransitionToState(PlayerFSMBaseState state)
        {
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
            rigidbody2D = GetComponent<Rigidbody2D>();

            DefaultGravityScale = rigidbody2D.gravityScale;

            TransitionToState(IdleState);
        }

        void Update()
        {
#if UNITY_ANDROID || UNITY_IPHONE
            horizontal = joystick.Horizontal * speed;
            vertical = joystick.Vertical * climbingSpeed;
#else
        horizontal = Input.GetAxisRaw("Horizontal") * speed;
        vertical = Input.GetAxisRaw("Vertical") * speed;
#endif
            checkCharacterRotation();

            currentState.Update(this);
        }

        private void FixedUpdate()
        {
            checkCharacterGrounded();
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

        void checkCharacterRotation()
        {
            if (isLookingRight && horizontal < 0)
            {
                Flip();
                isLookingRight = false;
            }
            else if (!isLookingRight && horizontal > 0)
            {
                Flip();
                isLookingRight = true;
            }
        }

        void checkCharacterGrounded()
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