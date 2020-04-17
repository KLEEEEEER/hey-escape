using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/*
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float jumpForce = 400f;
    private bool grounded;
    
    const float groundedRadius = .2f;
    const float ceilingRadius = .25f;

    public float defaultGravityScale;

    [SerializeField]private LayerMask groundLayers;

    [Range(0, 1)][SerializeField]private float crouchSpeedMultiplier = 1;
    [SerializeField]private bool airControl = false;
    [SerializeField]private Transform groundCheck;
    [SerializeField]private Transform ceilingCheck;
    [SerializeField]private Collider2D crouchDisableCollider;
    [SerializeField]private bool facingRight = true;
    Collider2D[] colliders;

    Rigidbody2D rigidbody2D;
    Vector2 velocity = Vector3.zero;
    [Range(0, .3f)][SerializeField] private float movementSmoothing = .05f;

    [SerializeField]private UnityEvent OnLandEvent;

    [SerializeField]private UnityEvent OnJumpEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    [SerializeField] private BoolEvent OnCrouchEvent;
    private bool wasCrouching = false;

    public bool jump = false;
    public bool crouch = false;
    public bool crouchButtonPressed = false;
    public bool isOnLadder = false;

    private float freezePositionY;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        defaultGravityScale = rigidbody2D.gravityScale;

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnJumpEvent == null)
            OnJumpEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;
        colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, groundLayers);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
                break;
            }
        }
    }

    public void Move(float horizontalMove, float verticalMove) 
    {
        crouch = (crouchButtonPressed || Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, groundLayers));

        if (grounded || airControl)
        {
            if (crouch)
            {
                if (!wasCrouching)
                {
                    wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                horizontalMove *= crouchSpeedMultiplier;

                if (crouchDisableCollider != null)
                    crouchDisableCollider.enabled = false;
            }
            else
            {
                if (crouchDisableCollider != null)
                    crouchDisableCollider.enabled = true;

                if (wasCrouching)
                {
                    wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            Vector2 targetVelocity = new Vector2(horizontalMove * 10f, rigidbody2D.velocity.y);
            rigidbody2D.velocity = Vector2.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

            if (horizontalMove > 0 && !facingRight)
            {
                Flip();
            }
            else if (horizontalMove < 0 && facingRight)
            {
                Flip();
            }
        }

        if (isOnLadder)
        {
            freezePositionY = rigidbody2D.position.y;

            rigidbody2D.gravityScale = 0f;
            rigidbody2D.velocity = new Vector2(0, 0);
            //rigidbody2D.position = new Vector2(rigidbody2D.position.x, rigidbody2D.position.x + verticalMove * 10f);
            transform.position = new Vector2(transform.position.x + horizontalMove / 6f, transform.position.y + verticalMove);
        }
        else
        {
            rigidbody2D.gravityScale = defaultGravityScale;
        }
    }

    public void Jump()
    {
        if (grounded && !crouch)
        {
            OnJumpEvent.Invoke();
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        *//*Vector3 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;*//*
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(ceilingCheck.position, ceilingRadius);
        Gizmos.DrawWireSphere(groundCheck.position, groundedRadius);
    }


}*/
