﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] float interactibleDetectionRadius = 0.4f;
    [SerializeField] private float enemyDetectionRadius = 1;
    [SerializeField] private Transform enemyDetectionPosition;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject eButton;
    [SerializeField] private GameObject qButton;

    [SerializeField] bool isHidden = false;

    Collider2D[] colliders;

    //public bool nearHidePlace = false;
    //private IHidePlace hidePlace;
    List<IInteractable> interactables;
    List<ISearchable> searchables;
    List<IKillable> killables;
    List<IHidePlace> hideplaces;
    IHidePlace currentHidePlace = null;

    public event Action<string> OnPlayerInteractEvent;

    private void Start()
    {
        interactables = new List<IInteractable>();
        searchables = new List<ISearchable>();
        killables = new List<IKillable>();
        hideplaces = new List<IHidePlace>();
    }

    void Update()
    {
        if (GameManager.instance.IsGameOver) return;

#if UNITY_STANDALONE

        if (!playerMovement.isPlayerMovingDisabled() && Input.GetKeyDown(KeyCode.Q) && killables.Count > 0)
        {
            foreach (IKillable killable in killables)
            {
                animator.SetTrigger("Kill");
                killable.Kill();
            }
        }

        if (!playerMovement.isPlayerMovingDisabled() && Input.GetKeyDown(KeyCode.E) && searchables.Count > 0)
        {
            animator.SetTrigger("Search");

            //List<InventoryItem> enemyItems = new List<InventoryItem>();
            foreach (ISearchable searchable in searchables)
            {
                List<InventoryItem> enemyItems = searchable.Search();
                if (enemyItems.Count > 0)
                {
                    foreach (InventoryItem item in enemyItems)
                    {
                        Inventory.instance.AddItem(item);
                    }
                }
            }
        }

        if (!playerMovement.isPlayerMovingDisabled() && Input.GetKeyDown(KeyCode.E))
        {
            foreach (IInteractable interactable in interactables)
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsGrounded", true);
                interactable.Interact();
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && !isHidden && hideplaces.Count > 0)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsGrounded", true);
            if (hideplaces[0].IsAccessible())
            {
                hideplaces[0].Hide();
                currentHidePlace = hideplaces[0];
                isHidden = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && isHidden && currentHidePlace != null)
        {
            currentHidePlace.Unhide();
            currentHidePlace = null;
            isHidden = false;
        }
#endif


        #if UNITY_ANDROID || UNITY_IPHONE
        if (GameManager.instance.CharacterController2D.Vertical > 0.8f && !isHidden && hideplaces.Count > 0)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsGrounded", true);
            if (hideplaces[0].IsAccessible())
            {
                hideplaces[0].Hide();
                currentHidePlace = hideplaces[0];
                isHidden = true;
            }
        }
        if (GameManager.instance.CharacterController2D.Vertical < 0.8f && isHidden && currentHidePlace != null)
        {
            currentHidePlace.Unhide();
            currentHidePlace = null;
            isHidden = false;
        }
#endif
    }

    private void FixedUpdate()
    {
        interactables.Clear();
        killables.Clear();
        searchables.Clear();
        hideplaces.Clear();

        colliders = Physics2D.OverlapCircleAll(enemyDetectionPosition.position, enemyDetectionRadius);
        foreach (Collider2D collider in colliders)
        {
            IKillable killable = collider.GetComponent<IKillable>();
            if (killable != null)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    if (!enemy.IsDead())
                        killables.Add(killable);
                }
                else
                {
                    killables.Add(killable);
                }
            }

            ISearchable searchable = collider.GetComponent<ISearchable>();
            if (searchable != null)
            {
                searchables.Add(searchable);
            }
        }

        colliders = Physics2D.OverlapCircleAll(transform.position, interactibleDetectionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;

            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactables.Add(interactable);
            }

            IHidePlace hideplace = collider.GetComponent<IHidePlace>();
            if (hideplace != null)
            {
                hideplaces.Add(hideplace);
            }
        }

        /*if (killables.Count > 0)
        {
            if (!qButton.activeSelf)
                qButton.SetActive(true);
        }
        else
        {
            qButton.SetActive(false);
        }

        if ((searchables.Count > 0) && !qButton.activeSelf)   // interactables.Count > 0 || 
        {
            if (!eButton.activeSelf)
                eButton.SetActive(true);
        }
        else
        {
            eButton.SetActive(false);
        }*/

        
    }

    public void KillButtonPressed()
    {
        if (playerMovement.isPlayerMovingDisabled() || GameManager.instance.IsGameOver) return;

        if (killables.Count > 0)
        {
            foreach (IKillable killable in killables)
            {
                animator.SetTrigger("Kill");
                killable.Kill();
            }
        }
    }
    public void UsingButtonPressed()
    {
        if (playerMovement.isPlayerMovingDisabled() || GameManager.instance.IsGameOver) return;

        if (searchables.Count > 0)
        {
            animator.SetTrigger("Search");

            foreach (ISearchable searchable in searchables)
            {
                List<InventoryItem> enemyItems = searchable.Search();
                if (enemyItems.Count > 0)
                {
                    foreach (InventoryItem item in enemyItems)
                    {
                        Inventory.instance.AddItem(item);
                    }
                }
            }
        }

        if (interactables.Count > 0)
        {
            foreach (IInteractable interactable in interactables)
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsGrounded", true);
                interactable.Interact();
            }
        }
    }

    public void HidePlayer()
    {
        isHidden = true;
    }

    public void UnhidePlayer()
    {
        isHidden = false;
    }

    public bool isPlayerHidden()
    {
        return isHidden;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyDetectionPosition.position, enemyDetectionRadius);

        Gizmos.DrawWireSphere(transform.position, interactibleDetectionRadius);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyNear = isEnemyNearCheck(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyNear = isEnemyNearCheck(collision);
    }

    private Enemy isEnemyNearCheck(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null) Debug.Log("Enemy near");
        else Debug.Log("Enemy left");
        return (enemy != null) ? enemy : null;
    }*/
}
