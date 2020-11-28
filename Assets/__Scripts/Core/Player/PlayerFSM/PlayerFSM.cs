﻿using Core.Detectors;
using HeyEscape.Core.Helpers;
using HeyEscape.Core.Player.FSM.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HeyEscape.Core.Player.FSM
{

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(InputHandler))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerFSM : MonoBehaviour
    {
        [SerializeField] private InputHandler inputHandler;
        public InputHandler InputHandler { get => inputHandler; set { inputHandler = value; } }

        [SerializeField] private PlayerMovement playerMovement;
        public PlayerMovement PlayerMovement { get => playerMovement; set { playerMovement = value; } }

        [SerializeField] private DetectorHandler detectorHandler;
        public DetectorHandler DetectorHandler { get => detectorHandler; set { detectorHandler = value; } }

        [Header("Components")]
        private Rigidbody2D rigidbody2D;
        public Rigidbody2D Rigidbody2D { get => rigidbody2D; }

        [SerializeField] private Animator animator;
        public Animator Animator { get => animator; set { animator = value; } }

        [SerializeField] PlayerAttributesSO playerAttributes;
        public PlayerAttributesSO PlayerAttributes { get => playerAttributes; }

        [SerializeField] RaycastColliderChecker groundAhead;
        public RaycastColliderChecker GroundAhead { get => groundAhead; }

        [SerializeField] CircleColliderChecker grounded;
        public CircleColliderChecker Grounded { get => grounded; }

        public Vector2 currentVelocity;
        public Transform CeilingCheck;
        public float CeilingRadius = .25f;
        [HideInInspector] public float DefaultGravityScale;
        public Joystick joystick;
        public bool IsMobileJumpPressed = false;

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

        public PlayerFSMIdleState IdleState;
        public PlayerFSMJumpingState JumpingState;
        public PlayerFSMRunningState RunningState;
        public PlayerFSMDuckingState DuckingState;
        public PlayerFSMDisableState DisableState;
        public PlayerFSMOnLadderState LadderState;
        public PlayerFSMInWindowState InWindowState;
        public PlayerFSMHiddenState HiddenState;

        [SerializeField] public GameObject arrow;

        public void TransitionToState(PlayerFSMBaseState state)
        {
            currentState.ExitState();
            currentState = state;
            currentState.EnterState();
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
            IdleState = new PlayerFSMIdleState(this);
            JumpingState = new PlayerFSMJumpingState(this);
            RunningState = new PlayerFSMRunningState(this);
            DuckingState = new PlayerFSMDuckingState(this);
            DisableState = new PlayerFSMDisableState(this);
            LadderState = new PlayerFSMOnLadderState(this);
            InWindowState = new PlayerFSMInWindowState(this);
            HiddenState = new PlayerFSMHiddenState(this);

            currentState = DisableState;

            rigidbody2D = GetComponent<Rigidbody2D>();

            DefaultGravityScale = rigidbody2D.gravityScale;

            TransitionToState(IdleState);
        }

        void Update()
        {
            currentState.Update();
        }

        private void FixedUpdate()
        {
            Animator.SetBool("IsGrounded", Grounded.Check());
            currentState.FixedUpdate();
        }

        private void LateUpdate()
        {
            currentState.LateUpdate();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            currentState.OnTriggerEnter2D(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            currentState.OnTriggerExit2D(collision);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(CeilingCheck.position, CeilingRadius);
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
    }
}