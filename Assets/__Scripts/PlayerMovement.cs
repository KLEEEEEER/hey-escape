using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    float Horizontal;
    float Vertical;
    public bool disableMovement = false;

    [SerializeField] float speed = 10;
    [SerializeField] float climbingSpeed = 5;
    [SerializeField] Joystick joystick;
    public bool isVisibleRight = false;
    public bool isVisibleLeft = false;

    [SerializeField] Collider2D[] collidersToDisable;
    [SerializeField] Rigidbody2D rigidbodyToDisable;

    private Animator animator;

    [SerializeField] private LayerMask ladderLayer;


    Collider2D[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
#if UNITY_ANDROID || UNITY_IPHONE
        joystick.gameObject.SetActive(true);
#else
        joystick.gameObject.SetActive(false);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (disableMovement)
        {
            ToggleColliders(disableMovement);
            animator.SetFloat("Speed", 0);
            return;
        }
        else
        {
            Move();
            ToggleColliders(disableMovement);
        }
    }

    

    private void Move()
    {
        if (GameManager.instance.IsGameOver) return;

#if UNITY_ANDROID || UNITY_IPHONE
        Horizontal = joystick.Horizontal * speed;
        Vertical = joystick.Vertical * climbingSpeed;
#else
        Horizontal = Input.GetAxisRaw("Horizontal") * speed;
        Vertical = Input.GetAxisRaw("Vertical") * climbingSpeed;
#endif
        animator.SetFloat("Speed", Mathf.Abs(Horizontal));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.Jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            controller.crouchButtonPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            controller.crouchButtonPressed = false;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
       animator.SetBool("IsCrouching", isCrouching);
    }

    public void OnStartJump()
    {
        animator.SetBool("IsJumping", true);
    }

    public bool isPlayerMovingDisabled()
    {
        return disableMovement;
    }

    private void FixedUpdate()
    {
        if (disableMovement) 
        {
            animator.SetFloat("Speed", 0);
            return;
        };
        controller.Move(Horizontal * Time.fixedDeltaTime, Vertical * Time.fixedDeltaTime);
    }

    private void ToggleColliders(bool enabled)
    {
        foreach (Collider2D collider in collidersToDisable)
        {
            collider.enabled = !enabled;
            rigidbodyToDisable.isKinematic = enabled;
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == gameObject) return;
        Debug.Log("OnTriggerStay2D");
        hidePlace = collision.GetComponent<IHidePlace>();
        if (hidePlace != null)
        {
            nearHidePlace = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == gameObject) return;
        Debug.Log("OnTriggerExit2D");
        hidePlace = collision.GetComponent<IHidePlace>();
        if (hidePlace != null)
        {
            nearHidePlace = false;
            hidePlace = null;
        }
    }*/

    public void OnGameOver()
    {
        disableMovement = true;
        rigidbodyToDisable.velocity = new Vector2(0, 0);
    }

    public void OnGameWon()
    {
        disableMovement = true;
        rigidbodyToDisable.velocity = new Vector2(0, 0);
    }

}
