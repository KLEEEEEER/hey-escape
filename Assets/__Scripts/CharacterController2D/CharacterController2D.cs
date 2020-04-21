using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    [Header("Character Attributes")]
    public float speed;
    public float ClimbingSpeedMultiplier;
    public Vector2 currentVelocity; //[HideInInspector] 
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

    private CharacterControllerBaseState currentState;

    public CharacterControllerBaseState CurrentState
    {
        get => currentState;
    }

    public readonly CharacterControllerIdleState IdleState = new CharacterControllerIdleState();
    public readonly CharacterControllerJumpingState JumpingState = new CharacterControllerJumpingState();
    public readonly CharacterControllerRunningState RunningState = new CharacterControllerRunningState();
    public readonly CharacterControllerDuckingState DuckingState = new CharacterControllerDuckingState();
    public readonly CharacterControllerDisableState DisableState = new CharacterControllerDisableState();
    public readonly CharacterControllerOnLadderState LadderState = new CharacterControllerOnLadderState();
    public readonly CharacterControllerInWindowState InWindowState = new CharacterControllerInWindowState();

    private float horizontal;
    private float vertical;
    public float Horizontal { get => horizontal; }
    public float Vertical { get => vertical; }
    private bool isLookingRight = true;

    public void TransitionToState(CharacterControllerBaseState state)
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

        Application.targetFrameRate = 60;

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
        checkCharacterGrounded();

        currentState.Update(this);
    }

    private void FixedUpdate()
    {
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
    }

    public void OnGameOver()
    {
        TransitionToState(DisableState);
    }
}
