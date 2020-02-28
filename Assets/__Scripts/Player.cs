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

    public bool nearHidePlace = false;
    private IHidePlace hidePlace;

    public event Action<string> OnPlayerInteractEvent;

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

        if (hidePlace != null && nearHidePlace && Input.GetKeyDown(KeyCode.E))
        {
            if (hidePlace.IsAccessible())
            {
                if (!isHidden)
                {
                    hidePlace.Hide(gameObject);
                    isHidden = true;
                }
                else
                {
                    hidePlace.Unhide(gameObject);
                    isHidden = false;
                }
            }
            else
            {
                OnPlayerInteractEvent.Invoke("Hiding place is not accessible");
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

        nearHidePlace = false;
        colliders = Physics2D.OverlapCircleAll(transform.position, interactibleDetectionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;
            hidePlace = collider.GetComponent<IHidePlace>();
            if (hidePlace != null)
            {
                nearHidePlace = true;
                break;
            }
        }
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
