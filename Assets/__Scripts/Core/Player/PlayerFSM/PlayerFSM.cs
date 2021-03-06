﻿using Cinemachine;
using HeyEscape.Core.Detectors;
using HeyEscape.Core.Helpers;
using HeyEscape.Core.Inventory;
using HeyEscape.Core.Player.FSM.Commands;
using HeyEscape.Core.Player.FSM.States;
using System.Collections;
using UnityEditor;
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
        public InputHandler InputHandler { get => inputHandler; }

        [SerializeField] private PlayerMovement playerMovement;
        public PlayerMovement PlayerMovement { get => playerMovement; }

        [SerializeField] private DetectorHandler detectorHandler;
        public DetectorHandler DetectorHandler { get => detectorHandler; }

        [Header("Components")]
        private Rigidbody2D rigidbody2D;
        public Rigidbody2D Rigidbody2D { get => rigidbody2D; }

        [SerializeField] private Animator animator;
        public Animator Animator { get => animator; }

        [SerializeField] PlayerAttributesSO playerAttributes;
        public PlayerAttributesSO PlayerAttributes { get => playerAttributes; }

        [SerializeField] RaycastColliderChecker groundAhead;
        public RaycastColliderChecker GroundAhead { get => groundAhead; }

        [SerializeField] CircleColliderChecker grounded;
        public CircleColliderChecker Grounded { get => grounded; }

        [SerializeField] VisibilityState visibility;
        public VisibilityState Visibility { get => visibility; }

        [SerializeField] Inventory.Inventory inventory;
        public Inventory.Inventory Inventory { get => inventory; }

        [SerializeField] SpriteRenderer renderer;
        public SpriteRenderer Renderer { get => renderer; }

        [SerializeField] Collider2D[] collidersToDisable;
        public Collider2D[] CollidersToDisable { get => collidersToDisable; }

        [SerializeField] CinemachineVirtualCamera virtualCamera;
        public CinemachineVirtualCamera VirtualCamera { get => virtualCamera; }

        [SerializeField] PlayerLightVision lightVision;
        public PlayerLightVision LightVision { get => lightVision; }

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
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        public PlayerFSMGodModeState GodModeState;
#endif

        [SerializeField] public GameObject arrow;

        // Commands
        private Command jumpCommand = new JumpCommand();
        public Command JumpCommand { get => jumpCommand; }
        private Command useCommand = new UseCommand();
        public Command UseCommand { get => useCommand; }
        private Command killCommand = new KillCommand();
        public Command KillCommand { get => killCommand; }
        private Command hideCommand = new HideCommand();
        public Command HideCommand { get => hideCommand; }

        public void TransitionToState(PlayerFSMBaseState state)
        {
            //Debug.Log($"Transitioning from {currentState.ToString()} to {state.ToString()}");

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
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            GodModeState = new PlayerFSMGodModeState(this);
#endif

            detectorHandler.Initialize(this);

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

        public void SetEnableColliders(bool enabled)
        {
            foreach (Collider2D collider in CollidersToDisable)
            {
                collider.enabled = enabled;
            }
        }
        IEnumerator disablingCoroutine;
        public void DisableForTime(float seconds)
        {
            Rigidbody2D.velocity = Vector2.zero;
            if (seconds < 0.001f) return;
            disablingCoroutine = DisableCoroutine(seconds);
            StartCoroutine(disablingCoroutine);
        }

        IEnumerator DisableCoroutine(float seconds)
        {
            TransitionToState(DisableState);
            yield return new WaitForSeconds(seconds);
            TransitionToState(IdleState);
        }

        public void OnGameOver()
        {
            Animator.SetTrigger("Caught");
            if (disablingCoroutine != null)
                StopCoroutine(disablingCoroutine);
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

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        public void ToggleGodMode()
        {
            if (currentState != GodModeState)
            {
                TransitionToState(GodModeState);
            }
            else
            {
                TransitionToState(IdleState);
            }
        }
#endif
    }
}