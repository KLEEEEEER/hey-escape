using System;
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

    [SerializeField] Enemy enemyNear;
    [SerializeField] bool isHidden = false;

    Collider2D[] colliders;

    //public bool nearHidePlace = false;
    //private IHidePlace hidePlace;
    List<IInteractable> interactables;

    public event Action<string> OnPlayerInteractEvent;

    private void Start()
    {
        interactables = new List<IInteractable>();
    }

    void Update()
    {
        if (enemyNear != null && !enemyNear.IsDead() && !playerMovement.isPlayerMovingDisabled() && Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("Kill");
            enemyNear.Die();
        }

        if (enemyNear != null && enemyNear.IsDead() && !playerMovement.isPlayerMovingDisabled() && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Search");
            List<InventoryItem> enemyItems = enemyNear.GetItems();
            if (enemyItems.Count > 0)
            {
                foreach (InventoryItem item in enemyItems)
                {
                    //Debug.Log("Enemy have " + item.Name);
                    Inventory.instance.AddItem(item);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (IInteractable interactable in interactables)
            {
                interactable.Interact();
            }
        }
    }

    private void FixedUpdate()
    {
        colliders = Physics2D.OverlapCircleAll(enemyDetectionPosition.position, enemyDetectionRadius);
        enemyNear = null;
        foreach (Collider2D collider in colliders)
        {
            Enemy enemyNearTemp = collider.gameObject.GetComponent<Enemy>();
            if (enemyNearTemp != null)
            {
                enemyNear = enemyNearTemp;
                break;
            }
        }

        interactables.Clear();
        colliders = Physics2D.OverlapCircleAll(transform.position, interactibleDetectionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactables.Add(interactable);
                break;
            }
        }
        Debug.Log("Found " + interactables.Count + " interactables");
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
